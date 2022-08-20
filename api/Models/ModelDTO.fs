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
