namespace FreeTimeRequestHandler

open DailyDos.Api.Services.AuthService
open DailyDos.Generated
open FreeTimeDao
open Microsoft.AspNetCore.Http
open Giraffe
open System.Linq

// TODO: needs Validations
module FreeTimeRequesthandler =
    /// create a new freetime for current user
    let PostFreeTime =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! freeTimeInput = ctx.BindJsonAsync<FreeTimeInput>()
                let userId = AuthService.GetUserIdFromContext ctx
                FreeTimeDao.InsertFreeTime userId freeTimeInput
                ctx.SetStatusCode 201
                return! json "created FreeTime" next ctx
            }

    /// update an existing freetime owned by current user
    let UpdateFreeTime (freeTimeId: int32) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! freeTimeInput = ctx.BindJsonAsync<FreeTimeInput>()
                FreeTimeDao.UpdateFreeTime freeTimeId freeTimeInput
                ctx.SetStatusCode 201
                return! json "updated FreeTime" next ctx
            }

    /// get a specific freetime object
    let GetFreeTime (freeTimeId: int32) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let freeTimeResult = (FreeTimeDao.GetFreeTimeById freeTimeId)
            if freeTimeResult.Count() < 1 then
                ctx.SetStatusCode 401
                json "no freetime found for this id" next ctx
            else
                ctx.SetStatusCode 200
                json (freeTimeResult.First()) next ctx

    /// get every freetime owned by current user
    let GetFreeTimes =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let freeTimeResult = FreeTimeDao.GetAllFreeTimes (AuthService.GetUserIdFromContext ctx)
            ctx.SetStatusCode 200
            json freeTimeResult next ctx

    /// delete an existing freetime
    let DeleteFreeTime (freeTimeId: int32) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
               FreeTimeDao.DeleteFreeTime freeTimeId
               return! json "ok" next ctx
            }
