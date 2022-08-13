namespace PlanRequesthandler

open DailyDos.Api.Daos.PlanDao
open DailyDos.Api.Services.AuthService
open DailyDos.Generated
open Giraffe
open Microsoft.AspNetCore.Http
open System.Linq

module PlanRequesthandler =
    let GetAllPlans: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) -> json "tried to get all plans for user" next ctx

    let GetPlan (id: int32) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let planEnumerator =
                PlanDao.GetPlanById id
            printf "Plan: %i" (planEnumerator.Count())

            if (planEnumerator.Count() = 0) then
                ctx.SetStatusCode 404
                json "no plan found" next ctx
            else
                json (planEnumerator.First()) next ctx


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
        fun (next: HttpFunc) (ctx: HttpContext) -> task { return! json $"tried to update plan {id}" next ctx }

    let DeletePlan (id: int32) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) -> task { return! json $"tried to delete plan {id}" next ctx }
