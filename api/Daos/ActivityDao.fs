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

    /// Create a new Activity
    let insert_activity id activity_view_model =
        insert {
            table "activities"

            value
                { user_id = id
                  name = activity_view_model.name
                  max_duration = activity_view_model.max_duration
                  min_duration = activity_view_model.min_duration
                  weekday_constraint = activity_view_model.weekday_constraint
                  recurring_type = activity_view_model.recurring_type
                  recurring_interval = activity_view_model.recurring_interval
                  create_time = DateTime.Now.ToUniversalTime() }
        }
        |> db_connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    /// Return specific Activity
    let get_activity_by_id (id: int) =
        select {
            table "activities"
            where (eq "id" id)
        }
        |> db_connection.SelectAsync<Activity>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let delete_activity_by_id (id: int) =
        delete {
            table "activities"
            where (eq "id" id)
        }
        |> db_connection.DeleteAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously