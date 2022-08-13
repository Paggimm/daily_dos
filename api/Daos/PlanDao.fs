module DailyDos.Api.Daos.PlanDao

open System
open System.Collections.Generic
open DailyDos.Generated
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL

open BaseDao

// TODO Map activities on plans
module PlanDao =
    let GetPlanById (id: int32) =
        select {
            table "plans"
            where (eq "id" id)
            take 1
        }
        |> db_connection.SelectAsync<Plan>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let GetAllPlansByUserId (userId: int32) =
        select {
            table "plans"
            where (eq "userId" userId)
        }
        |> db_connection.SelectAsync<Plan>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let InsertPlan (plan: PlanInput) (userId: int32) =
        insert {
            table "plans"
            value {|
                  userId = userId
                  activityId = plan.activityId
                  duration = plan.duration
                  date = plan.date.ToUniversalTime()
                  repeatable = plan.repeatable
                  createTime = DateTime.Now.ToUniversalTime()
                  |}
        }
        |> db_connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    let UpdatePlan (plan: PlanInput) (planId: int32) (userId: int32) =
            update {
                table "plans"
                where (eq "id" planId)
                set {|
                        userId = userId
                        activityId = plan.activityId
                        duration = plan.duration
                        date = plan.date.ToUniversalTime()
                        repeatable = plan.repeatable
                        createTime = DateTime.Now.ToUniversalTime()
                        |}
            }
            |> db_connection.UpdateAsync
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> ignore
