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

    let insert_new_activity: HttpHandler =
        fun (next: HttpFunc) (ctx: HttpContext) ->
            task {
                let! activity_view_model = ctx.BindJsonAsync<ActivityViewModel>()

                let id_claim =
                    ctx.User.Claims
                    |> Seq.find (fun c -> c.Type = CLAIM_TYPES.ID.ToString())

                let id = id_claim.Value |> int
                ActivityDao.insert_activity id activity_view_model
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
