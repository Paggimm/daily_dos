namespace DailyDos.Api.Services.AuthService

open Microsoft.AspNetCore.Identity
open System.Security.Claims
open Microsoft.AspNetCore.Http

open Consts.Consts
open DailyDos.Generated

module AuthService =
    /// returns the hashed Value of a given password
    let HashPassword user password =
        let passwordHasher = PasswordHasher<User>()
        passwordHasher.HashPassword(user, password)

    /// verifies the input password against the hashed password stored in the db
    let VerifyPassword user providedPassword storedPassword =
        let passwordHasher = PasswordHasher<User>()

        let result =
            passwordHasher.VerifyHashedPassword(user, storedPassword, providedPassword)

        if result.Equals PasswordVerificationResult.Success
           || result.Equals PasswordVerificationResult.SuccessRehashNeeded then
            true
        else
            false

    /// returns the user_id from the current user
    let GetUserIdFromContext (ctx: HttpContext) =
        let id_claim: Claim =
            ctx.User.Claims
            |> Seq.find (fun claim -> claim.Type = CLAIM_TYPES.ID.ToString())

        id_claim.Value |> int
