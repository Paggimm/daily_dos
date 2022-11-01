module DailyDos.Api.Daos.PlanDao

open System
open System.Linq
open DailyDos.Api.Daos.ActivityDao
open DailyDos.Api.Models.ModelDTO
open DailyDos.Generated
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL

open BaseDao

module PlanDao =
    let planTable = table'<PlanDTO> "plans"
    // TODO: doesn't feel right to create a special "insert" table definition
    // works for now but should be replaced by a more elegant solution later
    let planInserTable =
        table'<PlanInsertDTO> "plans"

    /// maps a PlanDTO and an Activity to a Plan-Model
    let private MapPlanActivity (planDTO: PlanDTO) (activity: Activity) : Plan =
        { id = planDTO.id
          userId = planDTO.userId
          activity = activity
          duration = planDTO.duration
          date = planDTO.date
          repeatable = planDTO.repeatable
          createTime = planDTO.createTime }

    // TODO throw error if nothing found
    /// retrieve a plan with its activity
    let GetPlanById (id: int32) =
        let connection = get_connection ()

        let planDTO =
            select {
                for plan in planTable do
                    where (plan.id = id)
                    take 1
            }
            |> connection.SelectAsync<PlanDTO>
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> Enumerable.First

        let activity =
            (ActivityDao.GetActivityById planDTO.activityId)
                .First()

        MapPlanActivity planDTO activity

    /// return a map of all given users plans
    let GetAllPlansByUserId (userId: int32) =
        let connection = get_connection ()

        let planDTOList =
            select {
                for plan in planTable do
                    where (plan.userId = userId)
            }
            |> connection.SelectAsync<PlanDTO>
            |> Async.AwaitTask
            |> Async.RunSynchronously

        let mutable activityMap =
            Map.empty<int, Activity>

        let planDTOSeq =
            Seq.cast<PlanDTO> planDTOList

        let mutable planMap = Map.empty<int, Plan>

        // search for activities for all planDTOs and build a list of plan models
        planDTOSeq
        |> Seq.iter (fun (planDTO: PlanDTO) ->
            if not (activityMap |> Map.containsKey planDTO.activityId) then
                let activityTemp =
                    (ActivityDao.GetActivityById planDTO.activityId)
                        .First()

                activityMap <-
                    activityMap
                    |> Map.add planDTO.activityId activityTemp

            let activity =
                activityMap[planDTO.activityId]

            let plan = MapPlanActivity planDTO activity
            planMap <- planMap |> Map.add plan.id plan)

        planMap

    /// create a new plan
    let InsertPlan (plan: PlanInput) (userId: int32) =
        let connection = get_connection ()

        let newPlan =
            { userId = userId
              activityId = plan.activityId
              duration = plan.duration
              date = plan.date.ToUniversalTime()
              repeatable = plan.repeatable
              createTime = DateTime.Now.ToUniversalTime() }

        insert {
            into planInserTable
            value newPlan
        }
        |> connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    let UpdatePlan (planInput: PlanInput) (planId: int32) =
        let connection = get_connection ()
        let oldPlan = (GetPlanById planId)

        let updatedPlan =
            { id = oldPlan.id
              userId = oldPlan.userId
              activityId = planInput.activityId
              duration = planInput.duration
              date = planInput.date.ToUniversalTime()
              repeatable = planInput.repeatable
              createTime = DateTime.Now.ToUniversalTime() }

        update {
            for plan in planTable do
                excludeColumn plan.id
                excludeColumn plan.userId
                set updatedPlan
                where (plan.id = planId)
        }
        |> connection.UpdateAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    /// Delete specific plan
    let DeletePlanById (id: int) =
        let connection = get_connection ()

        delete {
            for plan in planTable do
                where (plan.id = id)
        }
        |> connection.DeleteAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
