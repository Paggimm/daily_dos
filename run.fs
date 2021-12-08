open System
open System.IO

open Fake.Core
open Fake.IO

open RunHelpers
open RunHelpers.BasicShortcuts
open RunHelpers.FakeHelpers

[<RequireQualifiedAccess>]
module private Config =
    let codegenProject = "./codegen/codegen.fsproj"

    let serverProject = "./api/DailyDos.Api.fsproj"
    let generatedServerPath = "./api/generated"

    let clientFolder = "./vue_client/"

module private Task =
    let restoreTools () = Template.DotNet.toolRestore ()


    let restoreCodeGen () =
        Template.DotNet.restore Config.codegenProject

    let restoreServer () =
        Template.DotNet.restore Config.serverProject

    let restoreClient () = Template.Pnpm.install ()

    let buildCodegen () =
        Template.DotNet.build Config.codegenProject Template.DotNet.Debug

    let buildServer () =
        Template.DotNet.build Config.serverProject Template.DotNet.Debug

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

    let runClient () =
        CreateProcess.fromRawCommand "pnpm" [ "run"; "serve" ]
        |> CreateProcess.withWorkingDirectory Config.clientFolder
        |> Proc.runAsJob 10

    let runServer () =
        dotnet [
            "watch"
            "run"
            "-p"
            Config.serverProject
        ]

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
    let runClient () = Task.runClient ()
    let runServer () = Task.runServer ()

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
        | [ "client" ] -> Command.runClient ()
        | [ "server" ] -> Command.runServer ()
        | _ ->
            let msg =
                [ "Usage: dotnet run [<command>]"
                  "Look up available commands at the bottom of run.fs" ]

            Error(1, msg)
    |> ProcessResult.wrapUp