namespace AuthRequestHandler

open Consts.Consts
open FSharp.Control.Tasks
open Giraffe
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.AspNetCore.Http
open Microsoft.IdentityModel.Tokens
open System
open System.IdentityModel.Tokens.Jwt
open System.Security.Claims
open System.Text
open System.Linq

open DailyDos.Generated
open UserDao
open DailyDos.Api.Services.AuthService
open Consts

/// RequestHandler for Authentication-Purposes
module AuthRequestHandler =
    /// generate a jwt-Token from Input
    let private GenerateToken (user: User) =
        let claims =
            [| Claim(CLAIM_TYPES.NAME.ToString(), user.name)
               Claim(CLAIM_TYPES.ID.ToString(), user.id.ToString()) |]

        let expires = Nullable(DateTime.UtcNow.AddHours(168.0))
        let notBefore = Nullable(DateTime.UtcNow)

        let securityKey = SymmetricSecurityKey(Encoding.UTF8.GetBytes(Consts.secret))

        let signingCredentials =
            SigningCredentials(key = securityKey, algorithm = SecurityAlgorithms.HmacSha256)

        let token =
            JwtSecurityToken(
                issuer = "jwtwebapp.net",
                audience = "jwtwebapp.net",
                claims = claims,
                expires = expires,
                notBefore = notBefore,
                signingCredentials = signingCredentials
            )

        let tokenResult = { token = JwtSecurityTokenHandler().WriteToken(token) }

        tokenResult

    /// Login Requesthandler
    let HandlePostToken =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! model = ctx.BindJsonAsync<LoginViewModel>()
                let userPasswordResult = UserDao.GetUserPassword model.name

                if userPasswordResult.Count() = 0 then
                    let result = setStatusCode 401
                    return! result next ctx
                else
                    let userPassword = userPasswordResult.First()
                    // sadly we need a User for identity Password Object
                    let passwordHashingUser = { id = 0; name = model.name }

                    let passwordMatch =
                        AuthService.VerifyPassword passwordHashingUser model.password userPassword.password

                    let result =
                        if passwordMatch then
                            json (
                                GenerateToken(
                                    (UserDao.GetUserByName passwordHashingUser.name)
                                        .First()
                                )
                            )
                        else
                            setStatusCode 401

                    return! result next ctx
            }

    /// Trys to Authorize the Present JWT-Token
    let Authorize: HttpHandler =
        requiresAuthentication (challenge JwtBearerDefaults.AuthenticationScheme)

    ///
    let HandleGetSecured =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let email = ctx.User.FindFirst ClaimTypes.NameIdentifier

            text
                ("User "
                 + email.Value
                 + " is authorized to access this resource.")
                next
                ctx

    /// Register a new User
    let RegisterUser =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! registerData = ctx.BindJsonAsync<RegisterData>()
                let user: User = { id = 0; name = registerData.name }
                UserDao.InsertNewUser registerData.name (AuthService.HashPassword user registerData.password)
                return! next ctx
            }
