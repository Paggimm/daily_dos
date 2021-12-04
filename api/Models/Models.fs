namespace DailyDos.Api.Models

[<CLIMutable>]
type LoginViewModel = { email: string; password: string }

[<CLIMutable>]
type User = { id: int; name: string }

[<CLIMutable>]
type TokenResult = { token: string }
