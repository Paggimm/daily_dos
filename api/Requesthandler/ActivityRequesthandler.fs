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
    let get_all_activities: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let user_id = AuthService.get_user_id_from_context ctx
            let activities = ActivityDao.get_all_activities_by_user_id user_id
            json activities next ctx

    let post_activity: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! activity_view_model = ctx.BindJsonAsync<ActivityViewModel>()
                let user_id = AuthService.get_user_id_from_context ctx

                if ActivityInputValidator.validate_actitivy_input activity_view_model then
                    ActivityDao.insert_activity user_id activity_view_model
                    return! json "ok" next ctx
                else
                    ctx.SetStatusCode 400
                    return! json "refused" next ctx
            }

    let get_activity id : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let activity_enumerator = ActivityDao.get_activity_by_id id

            if (activity_enumerator.Count() = 0) then
                ctx.SetStatusCode 404
                json "no activity found" next ctx
            else
                json (activity_enumerator.First()) next ctx

    let delete_activity id : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            let old_activity = ActivityDao.get_activity_by_id id

            if (old_activity.Count() > 0
                && old_activity.First().user_id = AuthService.get_user_id_from_context ctx) then
                let result = ActivityDao.delete_activity_by_id id

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

    let patch_activity id : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! activity_view_model = ctx.BindJsonAsync<ActivityViewModel>()
                let user_id = AuthService.get_user_id_from_context ctx
                let old_activity = ActivityDao.get_activity_by_id id

                if old_activity.Count() > 0
                   && ActivityInputValidator.validate_activity_patch_input
                       activity_view_model
                       (old_activity.First())
                       user_id then
                    ActivityDao.update_activity id user_id activity_view_model
                    return! json "successfully updated activity" next ctx
                else
                    ctx.SetStatusCode 400
                    return! json "refused to patch activity" next ctx
            }
