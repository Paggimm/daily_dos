open Fake.Core

open RunHelpers
open RunHelpers.Shortcuts
open RunHelpers.Templates

let startAsJob (proc: CreateProcess<ProcessResult<unit>>) =
    printfn $"> %s{proc.CommandLine}"

    async {
        let! result = proc |> Proc.startAndAwait

        return
            match result.ExitCode with
            | 0 -> Ok
            | _ -> Error []
    }

type AsyncJobBuilder() =
    inherit ParallelJobBuilder()
    member __.YieldFrom x = x |> Async.RunSynchronously

let jobAsync = AsyncJobBuilder()

[<RequireQualifiedAccess>]
module private Config =
    let codegenProject = "./codegen/codegen.fsproj"

    let serverProject = "./api/DailyDos.Api.fsproj"
    let generatedServerPath = "./api/generated"

    let clientFolder = "./vue_client/"

    let ldeFolder = "./lde"

module private Task =
    let restoreTools () = DotNet.toolRestore ()


    let restoreCodeGen () = DotNet.restore Config.codegenProject

    let restoreServer () = DotNet.restore Config.serverProject

    let restoreClient () =
        CreateProcess.fromRawCommand "npm" [ "install" ]
        |> CreateProcess.withWorkingDirectory Config.clientFolder
        |> Job.fromCreateProcess

    let buildCodegen () =
        DotNet.build Config.codegenProject Debug

    let buildServer () = DotNet.build Config.serverProject Debug

    let buildClient () =
        CreateProcess.fromRawCommand "npm" [ "run"; "build" ]
        |> CreateProcess.withWorkingDirectory Config.clientFolder
        |> Job.fromCreateProcess

    let lintClient () =
        CreateProcess.fromRawCommand "npm" [ "run"; "lint" ]
        |> CreateProcess.withWorkingDirectory Config.clientFolder
        |> Job.fromCreateProcess

    let generate () =
        job {
            dotnet [ "run"; "--project"; Config.codegenProject ]
            // Format generated F# code
            dotnet [ "fantomas"; Config.generatedServerPath ]
        }

    let ldeStartCode () =
        jobAsync {
            CreateProcess.fromRawCommand "npm" [ "run"; "dev" ]
            |> CreateProcess.withWorkingDirectory Config.clientFolder
            |> startAsJob

            CreateProcess.fromRawCommand "dotnet" [ "watch"; "run"; "--project"; Config.serverProject ]
            |> startAsJob
        }

    let ldeStartFull () =
        job {
            CreateProcess.fromRawCommand
                "docker-compose"
                [ "-f"
                  $"%s{Config.ldeFolder}/db.docker-compose.yml"
                  "-f"
                  $"%s{Config.ldeFolder}/no-devcontainer.docker-compose.yml"
                  "up" ]
            |> Job.fromCreateProcess
        }

    let ldeDown () =
        job {
            CreateProcess.fromRawCommand
                "docker-compose"
                [ "-f"
                  $"%s{Config.ldeFolder}/db.docker-compose.yml"
                  "-f"
                  $"%s{Config.ldeFolder}/no-devcontainer.docker-compose.yml"
                  "down" ]
            |> Job.fromCreateProcess
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

    let ldeStartCode () =
        job {
            buildClient ()
            buildServer ()
            Task.ldeStartCode ()
        }

    let ldeStartFull () = Task.ldeStartFull ()
    let ldeDown () = Task.ldeDown ()

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
        | [ "start-code" ] -> Command.ldeStartCode ()
        | [ "start-full" ]
        | [ "start" ] -> Command.ldeStartFull ()
        | [ "down" ] -> Command.ldeDown ()
        | _ ->
            let msg =
                [ "Usage: dotnet run [<command>]"
                  "Look up available commands at the bottom of run.fs" ]

            Job.error msg
    |> Job.execute
