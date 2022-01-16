open System
open System.IO

open Fake.Core
open Fake.IO

open RunHelpers
open RunHelpers.BasicShortcuts
open RunHelpers.FakeHelpers
open RunHelpers.Templates

let startAsJob errorCode (proc: CreateProcess<ProcessResult<unit>>) =
    printfn $"> %s{proc.CommandLine}"
    let task = proc |> Proc.start

    async {
        let! result = task |> Async.AwaitTask

        return
            match result.ExitCode with
            | 0 -> Ok
            | _ -> Error(errorCode, [])
    }

let startAsJob' = startAsJob 10

type AsyncJobBuilder() =
    inherit JobBuilder()
    member __.YieldFrom x = x |> Async.RunSynchronously

let jobAsync = AsyncJobBuilder()

[<RequireQualifiedAccess>]
module private Config =
    let codegenProject = "./codegen/codegen.fsproj"

    let serverProject = "./api/DailyDos.Api.fsproj"
    let generatedServerPath = "./api/generated"

    let clientFolder = "./vue_client/"

    let leuFolder = "./leu"

module private Task =
    let restoreTools () = DotNet.toolRestore ()


    let restoreCodeGen () =
        DotNet.restore Config.codegenProject

    let restoreServer () =
        DotNet.restore Config.serverProject

    let restoreClient () = Pnpm.install ()

    let buildCodegen () =
        DotNet.build Config.codegenProject DotNetConfig.Debug

    let buildServer () =
        DotNet.build Config.serverProject DotNetConfig.Debug

    let buildClient () =
        CreateProcess.fromRawCommand "pnpm" [ "run"; "build" ]
        |> CreateProcess.withWorkingDirectory Config.clientFolder
        |> Proc.runAsJob 10

    let lintClient () =
        CreateProcess.fromRawCommand "pnpm" [ "run"; "lint" ]
        |> CreateProcess.withWorkingDirectory Config.clientFolder
        |> Proc.runAsJob 10

    let generate () =
        job {
            dotnet [
                "run"
                "-p"
                Config.codegenProject
            ]
            // Format generated F# code
            dotnet [
                "fantomas"
                Config.generatedServerPath
            ]
        }

    let leuStartCode () =
        jobAsync {
            let x =
                CreateProcess.fromRawCommand
                    "docker-compose"
                    [ "-f"
                      $"%s{Config.leuFolder}/db.docker-compose.yml"
                      "up" ]
                |> startAsJob'

            let y =
                CreateProcess.fromRawCommand "pnpm" [ "run"; "serve" ]
                |> CreateProcess.withWorkingDirectory Config.clientFolder
                |> startAsJob'

            let z =
                CreateProcess.fromRawCommand
                    "dotnet"
                    [ "watch"
                      "run"
                      "-p"
                      Config.serverProject ]
                |> startAsJob'

            yield! x
            yield! y
            yield! z
        }

    let leuStartFull () =
        job {
            CreateProcess.fromRawCommand
                "docker-compose"
                [ "-f"
                  $"%s{Config.leuFolder}/db.docker-compose.yml"
                  "-f"
                  $"%s{Config.leuFolder}/no-devcontainer.docker-compose.yml"
                  "up" ]
            |> Proc.runAsJob 10
        }

    let leuDown () =
        job {
            CreateProcess.fromRawCommand
                "docker-compose"
                [ "-f"
                  $"%s{Config.leuFolder}/db.docker-compose.yml"
                  "-f"
                  $"%s{Config.leuFolder}/no-devcontainer.docker-compose.yml"
                  "down" ]
            |> Proc.runAsJob 10
        }

module private Command =
    let buildClient () =
        job {
            Task.restoreClient ()
            Task.buildClient ()
        }

    let buildCodegen () =
        job {
            Task.restoreTools ()
            Task.restoreCodeGen ()
            Task.buildCodegen ()
        }

    let buildServer () =
        job {
            Task.restoreTools ()
            Task.restoreServer ()
            Task.buildServer ()
        }

    let lintClient () = Task.lintClient ()

    let setup () =
        job {
            Task.restoreTools ()
            Task.restoreCodeGen ()
            Task.restoreServer ()
            Task.restoreClient ()
        }

    let generate () = Task.generate ()

    let leuStartCode () =
        job {
            buildClient ()
            buildServer ()
            Task.leuStartCode ()
        }

    let leuStartFull () = Task.leuStartFull ()
    let leuDown () = Task.leuDown ()

[<EntryPoint>]
let main args =
    args
    |> List.ofArray
    |> function
        | [ "restore" ]
        | [ "setup" ] -> Command.setup ()
        | [ "build-client" ] -> Command.buildClient ()
        | [ "build-codegen" ] -> Command.buildCodegen ()
        | [ "build-server" ] -> Command.buildServer ()
        | [ "lint-client" ] -> Command.lintClient ()
        | [ "generate" ] -> Command.generate ()
        | [ "start-code" ] -> Command.leuStartCode ()
        | [ "start-full" ]
        | [ "start" ] -> Command.leuStartFull ()
        | [ "down" ] -> Command.leuDown ()
        | _ ->
            let msg =
                [ "Usage: dotnet run [<command>]"
                  "Look up available commands at the bottom of run.fs" ]

            Error(1, msg)
    |> ProcessResult.wrapUp
