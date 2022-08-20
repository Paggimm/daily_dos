namespace PlanRequesthandler

open DailyDos.Api.Daos.PlanDao
open DailyDos.Api.Services.AuthService
open DailyDos.Generated
open Giraffe
open Microsoft.AspNetCore.Http
open System.Linq

// TODO: Input and Ownership Validation!!!
module PlanRequesthandler =
    let GetAllPlans: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let userId = AuthService.GetUserIdFromContext ctx
            let planMap = PlanDao.GetAllPlansByUserId userId
            json planMap.Values next ctx

    let GetPlan (id: int32) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let plan =
                PlanDao.GetPlanById id
            json (plan) next ctx


    let PostPlan: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! planInput = ctx.BindJsonAsync<PlanInput>()
                let userId =
                    AuthService.GetUserIdFromContext ctx
                PlanDao.InsertPlan planInput userId
                return! json "ok" next ctx
            }

    let UpdatePlan (id: int32) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! planInput = ctx.BindJsonAsync<PlanInput>()
                PlanDao.UpdatePlan planInput id
                return! json "ok" next ctx
            }

    let DeletePlan (id: int32) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                PlanDao.DeletePlanById id
                return! json "ok" next ctx
            }
