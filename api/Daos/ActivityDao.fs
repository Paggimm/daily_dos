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
            where (eq "user_id" id)
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
                {| user_id = userId
                   name = activity.name
                   max_duration = activity.max_duration
                   min_duration = activity.min_duration
                   weekday_constraint = activity.weekday_constraint
                   recurring_type = activity.recurring_type
                   recurring_interval = activity.recurring_interval
                   create_time = DateTime.Now.ToUniversalTime() |}
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
    let UpdateActivity activityId userId activity =
        update {
            table "activities"
            where (eq "id" activityId)

            set
                {| user_id = userId
                   name = activity.name
                   max_duration = activity.max_duration
                   min_duration = activity.min_duration
                   weekday_constraint = activity.weekday_constraint
                   recurring_type = activity.recurring_type
                   recurring_interval = activity.recurring_interval
                   create_time = DateTime.Now.ToUniversalTime() |}
        }
        |> db_connection.UpdateAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
