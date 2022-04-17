namespace DailyDos.Api.Services.AuthService

open Microsoft.AspNetCore.Identity
open System.Security.Claims
open Microsoft.AspNetCore.Http

open Consts.Consts
open DailyDos.Generated

module AuthService =
    /// returns the hashed Value of a given password
    let hashPassword user password =
        let password_hasher = PasswordHasher<User>()
        password_hasher.HashPassword(user, password)

    /// verifies the input password against the hashed password stored in the db
    let verifyPassword user provided_password stored_password =
        let password_hasher = PasswordHasher<User>()

        let result =
            password_hasher.VerifyHashedPassword(user, stored_password, provided_password)

        if result.Equals PasswordVerificationResult.Success
           || result.Equals PasswordVerificationResult.SuccessRehashNeeded then
            true
        else
            false

    /// returns the user_id from the current user
    let get_user_id_from_context (ctx: HttpContext) =
        let id_claim: Claim =
            ctx.User.Claims
            |> Seq.find (fun claim -> claim.Type = CLAIM_TYPES.ID.ToString())

        id_claim.Value |> int
