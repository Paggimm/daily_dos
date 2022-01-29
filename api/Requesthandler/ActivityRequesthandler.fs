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
            (let id_claim = ctx.User.Claims |> Seq.find(fun c -> c.Type = CLAIM_TYPES.ID.ToString())
             let activities = ActivityDao.get_all_activities_by_user_id id_claim.Value
             json activities) next ctx

    let insert_new_activity: HttpHandler =
        fun (next:HttpFunc) (ctx: HttpContext) ->
            task {
                let! activity_view_model = ctx.BindJsonAsync<ActivityViewModel>()
                ActivityDao.insert_activity 1 activity_view_model.duration activity_view_model.name
                return! json "ok" next ctx
        }
