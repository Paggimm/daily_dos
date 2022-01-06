namespace DailyDos.Api.Services.AuthService

open Microsoft.AspNetCore.Identity
open DailyDos.Generated

module AuthService =
        /// returns the hashed Value of a given password
        let hashPassword user password =
            let password_hasher = PasswordHasher<User>()
            password_hasher.HashPassword(user, password)
