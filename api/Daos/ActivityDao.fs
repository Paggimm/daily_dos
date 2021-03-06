namespace ActivityDao

open DailyDos.Generated
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open System

open BaseDao

module ActivityDao =
    /// Return all Activities from a User
    let GetAllActivitiesByUserId (id: int) =
        select {
            table "activities"
            where (eq "userId" id)
        }
        |> db_connection.SelectAsync<Activity>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return specific Activity
    let GetActivityById (id: int) =
        select {
            table "activities"
            where (eq "id" id)
            take 1
        }
        |> db_connection.SelectAsync<Activity>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Create a new Activity
    let InsertActivity userId activity =
        insert {
            table "activities"

            value
                {| userId = userId
                   name = activity.name
                   maxDuration = activity.maxDuration
                   minDuration = activity.minDuration
                   weekdayConstraint = activity.weekdayConstraint
                   recurringType = activity.recurringType
                   recurringInterval = activity.recurringInterval
                   createTime = DateTime.Now.ToUniversalTime() |}
        }
        |> db_connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    /// Delete specific Activity
    let DeleteActivityById (id: int) =
        delete {
            table "activities"
            where (eq "id" id)
        }
        |> db_connection.DeleteAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Update specific Activity
    let UpdateActivity activityId userId activityInput =
        update {
            table "activities"
            where (eq "id" activityId)

            set
                {| userId = userId
                   name = activityInput.name
                   maxDuration = activityInput.maxDuration
                   minDuration = activityInput.minDuration
                   weekdayConstraint = activityInput.weekdayConstraint
                   recurringType = activityInput.recurringType
                   recurringInterval = activityInput.recurringInterval
                   createTime = DateTime.Now.ToUniversalTime() |}
        }
        |> db_connection.UpdateAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
