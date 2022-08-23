module DailyDos.Api.Models.ModelDTO

open System

[<CLIMutable>]
type PlanDTO = {
      id: int
      userId: int
      activityId: int
      duration: int
      date: DateTime
      repeatable: string
      createTime: DateTime
}

[<CLIMutable>]
type PlanInsertDTO = {
      userId: int
      activityId: int
      duration: int
      date: DateTime
      repeatable: string
      createTime: DateTime
}

[<CLIMutable>]
type UserDTO = {
    id: int
    name: string
    password: string
}

[<CLIMutable>]
type UserInsertDTO = {
    name: string
    password: string
}

[<CLIMutable>]
type ActivityDTO = {
      int: int
      name: string
      minDuration: int
      maxDuration: int
      weekdayConstraint: string
      recurringType: string
      recurringInterval: int
 }

[<CLIMutable>]
type ActivityInsertDTO = {
      userId: int
      name: string
      minDuration: int
      maxDuration: int
      weekdayConstraint: string
      recurringType: string
      recurringInterval: int
      createTime: DateTime
 }
