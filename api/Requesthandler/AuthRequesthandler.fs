namespace AuthRequestHandler

open FSharp.Control.Tasks
open Giraffe
open Microsoft.AspNetCore.Authentication.JwtBearer
open Microsoft.AspNetCore.Http
open Microsoft.IdentityModel.Tokens
open System
open System.IdentityModel.Tokens.Jwt
open System.Security.Claims
open System.Text

open DailyDos.Api.Models
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

                // TODO: add real authentication
                let result =
                    match model.email.ToLower(), model.password with
                    | ("bernie" as name, "123456")
                    | ("nico" as name, "654321") -> json (generateToken name)
                    | _ ->
                        // Invalid login data
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