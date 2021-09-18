namespace DailyDos.Api.Models

[<CLIMutable>]
type LoginViewModel = { email: string; password: string }

[<CLIMutable>]
type TokenResult = { token: string }
