namespace DailyDos.Generated

open System

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
      userId: int
      name: string
      minDuration: int
      maxDuration: int
      weekdayConstraint: string
      recurringType: string
      recurringInterval: int
      createTime: DateTime }
