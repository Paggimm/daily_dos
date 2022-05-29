namespace UserRequesthandler

open UserDao
open DailyDos.Generated
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks

/// RequestHandler for User-Interactions
module UserRequesthandler =
    /// Return a User by his ID
    let GetUserById (id: int) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) -> (UserDao.GetUserById id |> json) next ctx

    /// Return a List of all Users
    let GetAllUsers: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) -> (UserDao.GetAllUsers |> json) next ctx
