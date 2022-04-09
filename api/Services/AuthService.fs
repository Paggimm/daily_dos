namespace DailyDos.Api.Services.AuthService

open Microsoft.AspNetCore.Identity
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
