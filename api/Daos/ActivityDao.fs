namespace ActivityDao

open DailyDos.Generated
open Npgsql
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL

module ActivityDao =
    let private db_connection =
        new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=dailydos;User id=postgres;Password=postgres;")

    /// Return all Activities from a User
    let get_all_activities_by_user_id id =
        select {
            table "activities"
            where (eq "user_id" id)
        }
        |> db_connection.SelectAsync<Activity>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Create a new Activity
    let insert_activity user_id duration name =
        insert {
            table "activities"
            value {user_id = user_id; duration = duration; name = name}
        }
        |> db_connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
