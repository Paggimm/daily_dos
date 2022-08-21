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

[<CLIMutable>]
type ActivityInput =
    { name: string
      minDuration: int
      maxDuration: int
      weekdayConstraint: string
      recurringType: string
      recurringInterval: int }

[<CLIMutable>]
type Plan =
    { id: int
      userId: int
      activity: Activity
      duration: int
      date: DateTime
      repeatable: string
      createTime: DateTime }

[<CLIMutable>]
type PlanInput =
    { activityId: int
      duration: int
      date: DateTime
      repeatable: string }

[<CLIMutable>]
type PlanPostRating =
    { planId: int
      rating: int
      isPreRating: bool }
