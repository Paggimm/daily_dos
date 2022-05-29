namespace ActivityRequesthandler

open Giraffe
open Microsoft.AspNetCore.Http
open System.Linq
open FSharp.Control.Tasks

open DailyDos.Generated
open DailyDos.Validator.ActivityValidator
open ActivityDao
open DailyDos.Api.Services.AuthService

module ActivityRequesthandler =
    let GetAllActivities: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let userId = AuthService.GetUserIdFromContext ctx
            let activities = ActivityDao.GetAllActivitiesByUserId userId
            json activities next ctx

    let PostActivity: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! inputActivity = ctx.BindJsonAsync<Activity>()
                let userId = AuthService.GetUserIdFromContext ctx

                if ActivityInputValidator.ValidateActivityInput inputActivity then
                    ActivityDao.InsertActivity userId inputActivity
                    return! json "ok" next ctx
                else
                    ctx.SetStatusCode 400
                    return! json "refused" next ctx
            }

    let GetActivity id : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let activityEnumerator = ActivityDao.GetActivityById id

            if (activityEnumerator.Count() = 0) then
                ctx.SetStatusCode 404
                json "no activity found" next ctx
            else
                json (activityEnumerator.First()) next ctx

    let DeleteActivity id : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let oldActivity = ActivityDao.GetActivityById id

            if (oldActivity.Count() > 0
                && oldActivity.First().user_id = AuthService.GetUserIdFromContext ctx) then
                let result = ActivityDao.DeleteActivityById id

                match result with
                | 0 ->
                    ctx.SetStatusCode 404
                    json "activity not found " next ctx
                | 1 ->
                    ctx.SetStatusCode 200
                    json "activity succesfully deleted" next ctx
                | _ ->
                    ctx.SetStatusCode 500
                    json "an error occured" next ctx
            else
                ctx.SetStatusCode 403
                json "refused" next ctx

    let PatchActivity id : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! activity = ctx.BindJsonAsync<Activity>()
                let userId = AuthService.GetUserIdFromContext ctx
                let oldActivity = ActivityDao.GetActivityById id

                if oldActivity.Count() > 0
                   && ActivityInputValidator.ValidateActivityPatchInput activity (oldActivity.First()) userId then
                    ActivityDao.UpdateActivity id userId activity
                    return! json "successfully updated activity" next ctx
                else
                    ctx.SetStatusCode 400
                    return! json "refused to patch activity" next ctx
            }
