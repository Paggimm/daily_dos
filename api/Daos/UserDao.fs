namespace UserDao

open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open DailyDos.Generated
open DailyDos.Api.Models.ModelDTO

open BaseDao

/// Service Module for User related Querys
module UserDao =
    let userTable = table'<UserDTO> "users"
    // TODO: doesn't feel right to create a special "insert" table definition
    // works for now but should be replaced by a more elegant solution later
    let userInsertTable =
        table'<UserInsertDTO> "users"

    /// Return all Users found in the Database
    let GetAllUsers =
        let connection = get_connection ()

        select {
            for user in userTable do
                selectAll
        }
        |> connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return a User specified by his ID
    let GetUserById id =
        let connection = get_connection ()

        select {
            for user in userTable do
                where (user.id = id)
                take 1
        }
        |> connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return a Users Password
    let GetUserPassword userName =
        let connection = get_connection ()

        select {
            for user in userTable do
                where (user.name = userName)
                take 1
        }
        |> connection.SelectAsync<{| password: string |}>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return a User by his Username
    let GetUserByName name =
        let connection = get_connection ()

        select {
            for user in userTable do
                where (user.name = name)
                take 1
        }
        |> connection.SelectAsync<User>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Create a new user
    let InsertNewUser (name: string) (password: string) =
        let connection = get_connection ()

        insert {
            into userInsertTable
            value { name = name; password = password }
        }
        |> connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
