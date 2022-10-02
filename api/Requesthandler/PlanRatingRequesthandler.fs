namespace PlanRatingRequesthandler

open Giraffe
open Microsoft.AspNetCore.Http
open DailyDos.Generated
open PlanRatingDao
open System.Linq

module PlanRatingRequesthandler =
    /// create a new post rating
    let PostRating =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! ratingInput = ctx.BindJsonAsync<PlanRatingInput>()
                // TODO: validate input
                PlanRatingDao.InsertPlanRating ratingInput
                ctx.SetStatusCode 201
                return! json "ok" next ctx
            }

    /// update an existing post rating
    let UpdateRating (ratingId: int) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! ratingInput = ctx.BindJsonAsync<PlanRatingInput>()
                // TODO: validate input
                PlanRatingDao.UpdatePlanRating ratingId ratingInput
                ctx.SetStatusCode 201
                return! json "ok" next ctx
            }

    /// retrieve ratings for a single plan
    let GetRatings (planId: int) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            // TODO: validate ownership
            let planRating = (PlanRatingDao.GetAllRatingsForPlan planId)
            json planRating next ctx

    /// retrieve ratings for all plans with a given activity
    let GetRatingsForActivity (activityId: int) =
        fun (next: HttpFunc) (ctx: HttpContext) ->

            json "tried to get all ratings for a specific activity" next ctx

    let DeleteRating (ratingId: int) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
               // TODO: validate ownership and check existence
               PlanRatingDao.DeletePlanRating ratingId
               return! json "ok" next ctx
            }
