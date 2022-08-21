namespace PlanRequesthandler

open DailyDos.Api.Daos.PlanDao
open DailyDos.Api.InputValidator.PlanInputValidator
open DailyDos.Api.Services.AuthService
open DailyDos.Generated
open Giraffe
open Microsoft.AspNetCore.Http
open System.Linq

module PlanRequesthandler =
    /// return every plan of the current user
    let GetAllPlans: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let userId = AuthService.GetUserIdFromContext ctx
            let planMap = PlanDao.GetAllPlansByUserId userId
            json planMap.Values next ctx

    /// return an existing plan
    let GetPlan (id: int32) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let plan = PlanDao.GetPlanById id
            let userId = AuthService.GetUserIdFromContext ctx
            if plan.userId = userId then
                json (plan) next ctx
            else
                ctx.SetStatusCode 403
                json "forbidden" next ctx

    /// try to create a new plan
    let PostPlan: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! planInput = ctx.BindJsonAsync<PlanInput>()

                if PlanInputValidator.ValidatePlanInput planInput then
                    let userId = AuthService.GetUserIdFromContext ctx
                    PlanDao.InsertPlan planInput userId
                    return! json "ok" next ctx
                else
                    ctx.SetStatusCode 403
                    return! json "forbidden" next ctx
            }

    /// overwrite an existing plan
    let UpdatePlan (id: int32) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let userId = AuthService.GetUserIdFromContext ctx
                let! planInput = ctx.BindJsonAsync<PlanInput>()
                let oldPlan = PlanDao.GetPlanById id

                // we only do something when input is ok and user is the owner
                if PlanInputValidator.ValidatePlanInput planInput && oldPlan.userId = userId then
                    PlanDao.UpdatePlan planInput id
                    return! json "ok" next ctx
                else
                    ctx.SetStatusCode 403
                    return! json "forbidden" next ctx
            }

    /// delete an existing plan
    let DeletePlan (id: int32) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let userId = AuthService.GetUserIdFromContext ctx
                let plan = PlanDao.GetPlanById id

                if plan.userId = userId then
                    PlanDao.DeletePlanById id
                    return! json "ok" next ctx
                else
                    ctx.SetStatusCode 403
                    return! json "forbidden" next ctx
            }
