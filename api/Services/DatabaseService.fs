namespace DatabaseService

open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open DailyDos.Api.Models
open Npgsql

/// Service Module for User related Querys
module UserDatabaseService =
    let private db_connection =
        new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=testdb;User id=postgres;Password=postgres;")

    /// Return all Users found in the Database
    let get_all_users =
        select { table "users" }
        |> db_connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return a User specified by his ID
    let get_user_by_id id =
        select {
            table "users"
            where (eq "id" id)
        }
        |> db_connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously
