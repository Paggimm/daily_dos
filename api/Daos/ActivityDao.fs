namespace ActivityDao

open DailyDos.Generated
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open System

open BaseDao

module ActivityDao =
    /// Return all Activities from a User
    let get_all_activities_by_user_id (id: int) =
        select {
            table "activities"
            where (eq "user_id" id)
        }
        |> db_connection.SelectAsync<Activity>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return specific Activity
    let get_activity_by_id (id: int) =
        select {
            table "activities"
            where (eq "id" id)
            take 1
        }
        |> db_connection.SelectAsync<Activity>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Create a new Activity
    let insert_activity user_id activity =
        insert {
            table "activities"

            value
                {| user_id = user_id
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
    let delete_activity_by_id (id: int) =
        delete {
            table "activities"
            where (eq "id" id)
        }
        |> db_connection.DeleteAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Update specific Activity
    let update_activity activity_id user_id activity =
        update {
            table "activities"
            where (eq "id" activity_id)

            set
                {| user_id = user_id
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
