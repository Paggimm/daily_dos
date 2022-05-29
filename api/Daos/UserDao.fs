namespace UserDao

open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open DailyDos.Generated
open Npgsql

open BaseDao

/// Service Module for User related Querys
module UserDao =
    /// Return all Users found in the Database
    let GetAllUsers =
        select { table "users" }
        |> db_connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return a User specified by his ID
    let GetUserById id =
        select {
            table "users"
            where (eq "id" id)
        }
        |> db_connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return a Users Password
    let GetUserPassword userName =
        select {
            table "users"
            where (eq "name" userName)
            take 1
        }
        |> db_connection.SelectAsync<{| password: string |}>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return a User by his Username
    let GetUserByName name =
        select {
            table "users"
            where (eq "name" name)
            take 1
        }
        |> db_connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Create a new user
    let InsertNewUser (name: string) (password: string) =
        insert {
            table "users"
            value { name = name; password = password }
        }
        |> db_connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
