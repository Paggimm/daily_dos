namespace PlanRatingDao

open System
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open DailyDos.Generated
open DailyDos.Api.Models.ModelDTO
open Microsoft.FSharp.Core
open System.Linq

open BaseDao

module PlanRatingDao =
    let private planRatingInserTable =
        table'<PlanRatingDTO> "plan_ratings"

    let private planRatingTable =
        table'<PlanRating> "plan_ratings"

    /// return every PlanRating for a specific Plan
    let GetAllRatingsForPlan (planId: int) =
        let connection = get_connection ()

        select {
            for planRating in planRatingTable do
                where (planRating.planId = planId)
        }
        |> connection.SelectAsync<PlanRating>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// return a specific PlanRating
    let GetRatingById (ratingId: int) =
        let connection = get_connection ()

        select {
            for planRating in planRatingTable do
                where (planRating.id = ratingId)
                take 1
        }
        |> connection.SelectAsync<PlanRating>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// create a new PlanRating
    let InsertPlanRating (planInput: PlanRatingInput) =
        let connection = get_connection ()

        let newPlanRating: PlanRatingDTO =
            { planId = planInput.planId
              rating = planInput.rating
              isPreRating = planInput.isPreRating
              createTime = DateTime.Now.ToUniversalTime() }

        insert {
            into planRatingInserTable
            value newPlanRating
        }
        |> connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    /// delete a specifiec PlanRating
    let DeletePlanRating (ratingId: int) =
        let connection = get_connection ()

        delete {
            for planRating in planRatingTable do
                where (planRating.id = ratingId)
        }
        |> connection.DeleteAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    /// update PlanRating
    let UpdatePlanRating (planRatingId: int) (planInput: PlanRatingInput) =
        let connection = get_connection ()

        let oldPlanRating =
            (GetRatingById planRatingId).First()

        let updatedPlanRating: PlanRating =
            { id = oldPlanRating.id
              planId = oldPlanRating.planId
              rating = planInput.rating
              isPreRating = planInput.isPreRating
              createTime = oldPlanRating.createTime }

        update {
            for planRating in planRatingTable do
                set updatedPlanRating
                excludeColumn planRating.id
                excludeColumn planRating.planId
                excludeColumn planRating.createTime
        }
        |> connection.UpdateAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
