namespace UserDao

open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open DailyDos.Generated
open Npgsql

open BaseDao

/// Service Module for User related Querys
module UserDao =
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

    /// Return a LoginViewModel by Username
    let get_login_viewmodel_by_name name =
        select {
            table "users"
            where (eq "name" name)
            take 1
        }
        |> db_connection.SelectAsync<LoginViewModel>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return a User by his Username
    let get_user_by_name name =
        select {
            table "users"
            where (eq "name" name)
            take 1
        }
        |> db_connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Create a new user
    let insert_new_user (name: string) (password: string) =
        insert {
            table "users"
            value { name = name; password = password }
        }
        |> db_connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
