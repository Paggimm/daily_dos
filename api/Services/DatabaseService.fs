namespace DatabaseService

open MySql.Data.MySqlClient
open Dapper.FSharp
open Dapper.FSharp.MySQL
open DailyDos.Api.Models

module UserDatabaseService =
    let con: MySqlConnection =
        let conn_string =
            "Server=localhost;Database=dailydos;User=admin;Password=admin;"

        new MySqlConnection(conn_string)

    let get_all_users =
        select { table "user" }
        |> con.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    let get_user_by_id id =
        select {
            table "user"
            where (eq "id" id)
        }
        |> con.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously
