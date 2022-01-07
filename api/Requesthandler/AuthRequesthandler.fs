namespace AuthRequestHandler

open FSharp.Control.Tasks
open Giraffe
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Identity
open Microsoft.IdentityModel.Tokens
open System
open System.IdentityModel.Tokens.Jwt
open System.Security.Claims
open System.Text
open System.Linq

open DailyDos.Generated
open DatabaseService
open DailyDos.Api.Services.AuthService
open Consts

/// RequestHandler for Authentication-Purposes
module AuthRequestHandler =
    /// generate a jwt-Token from Input
    let private generateToken email =
        let claims =
            [|  Claim(JwtRegisteredClaimNames.Sub, email)
                Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) |]

        let expires = Nullable(DateTime.UtcNow.AddHours(1.0))
        let notBefore = Nullable(DateTime.UtcNow)

        let securityKey =
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(Consts.secret))

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

        let tokenResult =
            { token = JwtSecurityTokenHandler().WriteToken(token) }

        tokenResult

    /// Login Requesthandler
    let handlePostToken =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! model = ctx.BindJsonAsync<LoginViewModel>()
                let user_enumerator = UserDatabaseService.get_loginviewmodel_by_name model.name

                if user_enumerator.Count() = 0
                then
                    let result = setStatusCode 401
                    return! result next ctx
                else
                    let user: LoginViewModel = user_enumerator.First()
                    let db_user = { id=0; name=user.name }
                    let password_match = AuthService.verifyPassword db_user model.password user.password;
                    let result =
                        if password_match then
                            json (generateToken user.name)
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
            let email =
                ctx.User.FindFirst ClaimTypes.NameIdentifier

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
                let user: User = {id = 0; name = register_data.name}
                UserDatabaseService.insert_new_user register_data.name (AuthService.hashPassword user register_data.password)
                return! next ctx
        }
