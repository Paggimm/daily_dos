namespace PlanRequesthandler

open Giraffe
open Microsoft.AspNetCore.Http

module PlanRequesthandler =
    let GetAllPlans: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            json "tried to get all plans for user" next ctx

    let PostPlan: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                return! json "tried to post plan" next ctx
            }
