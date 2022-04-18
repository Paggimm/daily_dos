namespace AuthRequestHandler

open Consts.Consts
open FSharp.Control.Tasks
open Giraffe
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.AspNetCore.Http
open Microsoft.IdentityModel.JsonWebTokens
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
    let private generateToken (user: User) =
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
    let handlePostToken =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! model = ctx.BindJsonAsync<LoginViewModel>()
                let user_password_result = UserDao.get_user_password model.name

                if user_password_result.Count() = 0 then
                    let result = setStatusCode 401
                    return! result next ctx
                else
                    let user_password = user_password_result.First()
                    // sadly we need a User for identity Password Object
                    let password_hashing_user = { id = 0; name = model.name }

                    let password_match =
                        AuthService.verifyPassword password_hashing_user model.password user_password.password

                    let result =
                        if password_match then
                            json (
                                generateToken (
                                    (UserDao.get_user_by_name password_hashing_user.name)
                                        .First()
                                )
                            )
                        else
                            setStatusCode 401

                    return! result next ctx
            }

    /// Trys to Authorize the Present JWT-Token
    let authorize: HttpHandler =
        requiresAuthentication (challenge JwtBearerDefaults.AuthenticationScheme)

    ///
    let handleGetSecured =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let email = ctx.User.FindFirst ClaimTypes.NameIdentifier

            text
                ("User "
                 + email.Value
                 + " is authorized to access this resource.")
                next
                ctx

    /// Register a new User
    let registerUser =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! register_data = ctx.BindJsonAsync<RegisterData>()
                let user: User = { id = 0; name = register_data.name }
                UserDao.insert_new_user register_data.name (AuthService.hashPassword user register_data.password)
                return! next ctx
            }
