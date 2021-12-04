namespace DatabaseService

open Giraffe
open Microsoft.AspNetCore.Http

module UserRequesthandler =
    let get_user_by_id (id: int) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            (DatabaseService.UserDatabaseService.get_user_by_id id
             |> json)
                next
                ctx

    let get_all_users: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            (DatabaseService.UserDatabaseService.get_all_users
             |> json)
                next
                ctx
