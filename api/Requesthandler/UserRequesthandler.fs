namespace UserRequesthandler

open UserDao
open DailyDos.Generated
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks

/// RequestHandler for User-Interactions
module UserRequesthandler =
    /// Return a User by his ID
    let get_user_by_id (id: int) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            (UserDao.get_user_by_id id
             |> json) next ctx

    /// Return a List of all Users
    let get_all_users: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            (UserDao.get_all_users
             |> json) next ctx
