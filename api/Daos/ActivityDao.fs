namespace DailyDos.Api.Daos.ActivityDao

open DailyDos.Api.Models.ModelDTO
open DailyDos.Generated
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open System
open System.Linq

open BaseDao

module ActivityDao =
    let activityTable =
        table'<Activity> "activities"
    // TODO: doesn't feel right to create a special "insert" table definition
    // works for now but should be replaced by a more elegant solution later
    let activityInsertTable =
        table'<ActivityInsertDTO> "activities"

    /// checks if an activity actually exists
    let DoesActivityExist id =
        let connection = get_connection ()

        /// when we only want to know of a record exists we only try to retrieve an id from db to reduce data
        let result =
            select {
                for activity in activityTable do
                    where (activity.id = id)
                    take 1
            }
            |> connection.SelectAsync<{| id: int |}>
            |> Async.AwaitTask
            |> Async.RunSynchronously

        result.Count() > 0

    /// Return all Activities from a User
    let GetAllActivitiesByUserId (id: int) =
        let connection = get_connection ()

        select {
            for activity in activityTable do
                where (activity.userId = id)
        }
        |> connection.SelectAsync<Activity>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Return specific Activity
    let GetActivityById (id: int) =
        let connection = get_connection ()

        select {
            for activity in activityTable do
                where (activity.id = id)
                take 1
        }
        |> connection.SelectAsync<Activity>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Create a new Activity
    /// TODO: check if it works out with id=0
    let InsertActivity userId activity =
        let connection = get_connection ()

        let newActivity: ActivityInsertDTO =
            { userId = userId
              name = activity.name
              maxDuration = activity.maxDuration
              minDuration = activity.minDuration
              weekdayConstraint = activity.weekdayConstraint
              recurringType = activity.recurringType
              recurringInterval = activity.recurringInterval
              createTime = DateTime.Now.ToUniversalTime() }

        insert {
            into activityInsertTable
            value newActivity
        }
        |> connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    /// Delete specific Activity
    let DeleteActivityById (id: int) =
        let connection = get_connection ()

        delete {
            for activity in activityTable do
                where (activity.id = id)
        }
        |> connection.DeleteAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// Update specific Activity
    let UpdateActivity activityId activityInput =
        let connection = get_connection ()

        let oldActivity =
            (GetActivityById activityId).First()

        let updatedActivity: Activity =
            { id = oldActivity.id
              userId = oldActivity.userId
              name = activityInput.name
              maxDuration = activityInput.maxDuration
              minDuration = activityInput.minDuration
              weekdayConstraint = activityInput.weekdayConstraint
              recurringType = activityInput.recurringType
              recurringInterval = activityInput.recurringInterval
              createTime = oldActivity.createTime }

        update {
            for activity in activityTable do
                set updatedActivity
                excludeColumn activity.id
                excludeColumn activity.userId
                excludeColumn activity.createTime
                where (activity.id = activityId)
        }
        |> connection.UpdateAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
