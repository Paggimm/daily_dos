namespace DailyDos.Generated

[<CLIMutable>]
type LoginResponse = { token: string }

[<CLIMutable>]
type User = { id: int; name: string }

[<CLIMutable>]
type RegisterData =
    { name: string
      email: string
      password: string }

[<CLIMutable>]
type LoginViewModel = { name: string; password: string }

[<CLIMutable>]
type Activity =
    { id: int
      user_id: int
      duration: int
      name: string }

[<CLIMutable>]
type ActivityViewModel =
    { user_id: int
      duration: int
      name: string }
