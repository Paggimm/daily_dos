namespace ActivityRequesthandler

open System.Security.Claims
open System.Threading.Tasks
open Consts.Consts
open DailyDos.Generated
open Giraffe
open Microsoft.AspNetCore.Http
open System.Linq
open FSharp.Control.Tasks

open ActivityDao

module ActivityRequesthandler =
    let get_all_activities: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            (let id_claim =
                ctx.User.Claims
                |> Seq.find (fun c -> c.Type = CLAIM_TYPES.ID.ToString())

             let activities = ActivityDao.get_all_activities_by_user_id (id_claim.Value |> int)
             json activities)
                next
                ctx

    let post_activity: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! activity_view_model = ctx.BindJsonAsync<ActivityViewModel>()

                let id_claim =
                    ctx.User.Claims
                    |> Seq.find (fun claim -> claim.Type = CLAIM_TYPES.ID.ToString())

                let user_id = id_claim.Value |> int
                ActivityDao.insert_activity user_id activity_view_model
                return! json "ok" next ctx
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

    let patch_activity id : HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! activity_view_model = ctx.BindJsonAsync<ActivityViewModel>()

                let id_claim =
                    ctx.User.Claims
                    |> Seq.find (fun claim -> claim.Type = CLAIM_TYPES.ID.ToString())

                let user_id = id_claim.Value |> int
                ActivityDao.update_activity id user_id activity_view_model
                return! json "successfully updated activity" next ctx
            }
