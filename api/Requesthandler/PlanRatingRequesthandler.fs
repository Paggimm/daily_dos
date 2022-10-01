namespace PlanRatingRequesthandler

open Giraffe
open Microsoft.AspNetCore.Http

module PlanRatingRequesthandler =
    let PostRating =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                return! json "tried to create a new rating" next ctx
            }

    let UpdateRating (id: int) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                return! json "tried to update rating" next ctx
            }

    /// retrieve ratings for a single plan
    let GetRatings (id: int) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            json "tried to get the ratings for a specific plan" next ctx

    /// retrieve ratings for all plans with a given activity
    let GetRatingsForActivity (id: int) =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            json "tried to get all ratings for a specific activity" next ctx

    let DeleteRating (id: int) : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
               return! json "tried to delete rating" next ctx
            }
