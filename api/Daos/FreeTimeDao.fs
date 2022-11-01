namespace FreeTimeDao

open System
open Dapper.FSharp
open Dapper.FSharp.PostgreSQL
open DailyDos.Generated
open DailyDos.Api.Models.ModelDTO
open Microsoft.FSharp.Core
open System.Linq

open BaseDao

module FreeTimeDao =
    let private FreeTimeInsertTable =
        table'<FreeTimeDTO> "freetime"

    let private FreeTimeTable =
        table'<FreeTime> "freetime"

    /// return every FreeTime for a specific User
    let GetAllFreeTimes (userId: int) =
        let connection = get_connection ()

        select {
            for freeTime in FreeTimeTable do
                where (freeTime.userId = userId)
        }
        |> connection.SelectAsync<FreeTime>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// return a specific FreeTime
    let GetFreeTimeById (freeTimeID: int) =
        let connection = get_connection ()

        select {
            for freeTime in FreeTimeTable do
                where (freeTime.id = freeTimeID)
                take 1
        }
        |> connection.SelectAsync<FreeTime>
        |> Async.AwaitTask
        |> Async.RunSynchronously

    /// create a new FreeTime
    let InsertFreeTime (userId: int) (freeTimeInput: FreeTimeInput) =
        let connection = get_connection ()

        let newFreeTime: FreeTimeDTO =
            { userId = userId
              startDate = freeTimeInput.startDate
              duration = freeTimeInput.duration
              recurringType = freeTimeInput.recurringType
              recurringInterval = freeTimeInput.recurringInterval
              createTime = DateTime.Now.ToUniversalTime() }

        insert {
            into FreeTimeInsertTable
            value newFreeTime
        }
        |> connection.InsertAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    /// delete a specific FreeTime
    let DeleteFreeTime (freeTimeId: int) =
        let connection = get_connection ()

        delete {
            for freeTime in FreeTimeTable do
                where (freeTime.id = freeTimeId)
        }
        |> connection.DeleteAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore

    /// update FreeTime
    let UpdateFreeTime (freeTimeId: int) (freeTimeInput: FreeTimeInput) =
        let connection = get_connection ()

        let oldFreeTime =
            (GetFreeTimeById freeTimeId).First()

        let updatedFreeTime: FreeTime =
            { id = oldFreeTime.id
              userId = oldFreeTime.userId
              startDate = freeTimeInput.startDate
              duration = freeTimeInput.duration
              recurringType = freeTimeInput.recurringType
              recurringInterval = freeTimeInput.recurringInterval
              createTime = oldFreeTime.createTime }

        update {
            for freeTime in FreeTimeTable do
                where (freeTime.id = freeTimeId)
                set updatedFreeTime
                excludeColumn freeTime.id
                excludeColumn freeTime.userId
                excludeColumn freeTime.createTime
        }
        |> connection.UpdateAsync
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
