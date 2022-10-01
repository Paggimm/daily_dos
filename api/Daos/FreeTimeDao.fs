namespace FreeTimeDao

open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open DailyDos.Generated
open DailyDos.Api.Models.ModelDTO
open Microsoft.FSharp.Core
open System.Linq

open BaseDao

module FreeTimeDao =
    let private FreeTimeInsertTable = table'<FreeTimeDTO> "freetime"
    let private FreeTimeTable = table'<FreeTime> "freetime"

    /// return every FreeTime for a specific User
    let GetAllFreeTimes (userId: int) =
        select {
            for freeTime in FreeTimeTable do
                where (freeTime.userId = userId)
        }
        |> db_connection.SelectAsync<FreeTime>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// return a specific FreeTime
    let GetFreeTimeById (freeTimeID: int) =
        select {
            for freeTime in FreeTimeTable do
                where (freeTime.id = freeTimeID)
                take 1
        }
        |> db_connection.SelectAsync<FreeTime>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// create a new FreeTime
    let InsertFreeTime (userId: int)(freeTimeInput: FreeTimeInput) =
        let newFreeTime: FreeTimeDTO =
            {
                userId = userId
                startDate = freeTimeInput.startDate
                duration = freeTimeInput.duration
                recurringType = freeTimeInput.recurringType
                recurringInterval = freeTimeInput.recurringInterval
            }

        insert {
            into FreeTimeInsertTable
            value newFreeTime
        }
        |> db_connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    /// delete a specific FreeTime
    let DeleteFreeTime (freeTimeId: int) =
        delete {
            for freeTime in FreeTimeTable do
                where (freeTime.id = freeTimeId)
        }
        |> db_connection.DeleteAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// update FreeTime
    let UpdateFreeTime (freeTimeId: int) (freeTimeInput: FreeTimeInput) =
        let oldFreeTime = (GetFreeTimeById freeTimeId).First()
        let updatedFreeTime: FreeTime =
            {
                id = oldFreeTime.id
                userId = oldFreeTime.userId
                startDate = freeTimeInput.startDate
                duration = freeTimeInput.duration
                recurringType = freeTimeInput.recurringType
                recurringInterval = freeTimeInput.recurringInterval
                createTime = oldFreeTime.createTime
            }
        update {
            for freeTime in FreeTimeTable do
                set updatedFreeTime
                excludeColumn freeTime.id
                excludeColumn freeTime.userId
                excludeColumn freeTime.createTime
        }
        |> db_connection.UpdateAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

