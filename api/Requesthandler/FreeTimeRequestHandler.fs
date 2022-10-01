namespace FreeTimeRequestHandler

open Microsoft.AspNetCore.Http
open Giraffe

// TODO: needs implementations
module FreeTimeRequesthandler =
    /// create a new freetime for current user
    let PostFreeTime =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                return! json "tried to create a freetime" next ctx
            }

    /// update an existing freetime owned by current user
    let UpdateFreeTime (id: int32) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                return! json "tried to update freetime" next ctx
            }

    /// get a specific freetime object
    let GetFreeTime (id: int32) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            json "tried to get a specific freetime" next ctx

    /// get every freetime owned by current user
    let GetFreeTimes =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            json "tried to get all freetimes for current user" next ctx

    let DeleteFreeTime (id: int32) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
               return! json "tried to delete freetime" next ctx
            }
