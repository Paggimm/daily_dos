module DailyDos.Api.Daos.PlanDao

open System
open System.Linq
open ActivityDao
open DailyDos.Api.Models.ModelDTO
open DailyDos.Generated
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL

open BaseDao

module PlanDao =

    /// maps a PlanDTO and an Activity to a Plan-Model
    let private MapPlanActivity (planDTO: PlanDTO) (activity: Activity): Plan = {
            id = planDTO.id
            userId = planDTO.userId
            activity = activity
            duration = planDTO.duration
            date = planDTO.date
            repeatable = planDTO.repeatable
            createTime = planDTO.createTime
    }

    // TODO throw error if nothing found
    /// retrieve a plan with its activity
    let GetPlanById (id: int32) =
        let planDTO =
            select {
                table "plans"
                where (eq "id" id)
                take 1
            }
            |> db_connection.SelectAsync<PlanDTO>
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> Enumerable.First

        let activity = (ActivityDao.GetActivityById planDTO.activityId).First()
        MapPlanActivity planDTO activity

    /// return a map of all given users plans
    let GetAllPlansByUserId (userId: int32) =
        let planDTOList =
            select {
            table "plans"
            where (eq "userId" userId)
            }
            |> db_connection.SelectAsync<PlanDTO>
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let mutable activityMap = Map.empty<int, Activity>
        let planDTOSeq = Seq.cast<PlanDTO> planDTOList
        let mutable planMap = Map.empty<int, Plan>

        // search for activities for all planDTOs and build a list of plan models
        planDTOSeq
        |> Seq.iter (
            fun (planDTO: PlanDTO) ->
                if not (activityMap |> Map.containsKey planDTO.activityId) then
                    let activityTemp = (ActivityDao.GetActivityById planDTO.activityId).First()
                    activityMap <- activityMap |> Map.add planDTO.activityId  activityTemp

                let activity =  activityMap[planDTO.activityId]
                let plan = MapPlanActivity planDTO activity
                planMap <- planMap |> Map.add plan.id plan
            )
        planMap

    /// create a new plan
    let InsertPlan (plan: PlanInput) (userId: int32) =
        insert {
            table "plans"
            value {|
                  userId = userId
                  activityId = plan.activityId
                  duration = plan.duration
                  date = plan.date.ToUniversalTime()
                  repeatable = plan.repeatable
                  createTime = DateTime.Now.ToUniversalTime()
                  |}
        }
        |> db_connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    let UpdatePlan (plan: PlanInput) (planId: int32) =
            update {
                table "plans"
                where (eq "id" planId)
                set {|
                        activityId = plan.activityId
                        duration = plan.duration
                        date = plan.date.ToUniversalTime()
                        repeatable = plan.repeatable
                        createTime = DateTime.Now.ToUniversalTime()
                        |}
            }
            |> db_connection.UpdateAsync
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> ignore

    /// Delete specific Activity
    let DeletePlanById (id: int) =
        delete {
            table "plans"
            where (eq "id" id)
        }
        |> db_connection.DeleteAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
