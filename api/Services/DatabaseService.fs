namespace DatabaseService

open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open DailyDos.Api.Models
open Npgsql

module UserDatabaseService =
    let db_connection =
        new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=testdb;User id=postgres;Password=postgres;")

    let get_all_users =
        select { table "users" }
        |> db_connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let get_user_by_id id =
        select {
            table "users"
            where (eq "id" id)
        }
        |> db_connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously
