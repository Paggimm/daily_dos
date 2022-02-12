namespace ActivityDao

open DailyDos.Generated
open Npgsql
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open System

module ActivityDao =
    let private db_connection =
        new NpgsqlConnection("Server=postgres;Port=5432;Database=dailydos;User id=postgres;Password=postgres;")

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
                  create_time =
                    (DateTime.Now.Subtract(new DateTime(1970, 1, 1)))
                        .TotalSeconds
                    |> int }
        }
        |> db_connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
