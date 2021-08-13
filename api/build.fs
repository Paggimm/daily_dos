open Fake.Core
open Fake.DotNet
open Fake.IO

let initializeContext () =
    let execContext = Context.FakeExecutionContext.Create false "build.fsx" [ ]
    Context.setExecutionContext (Context.RuntimeContext.Fake execContext)

initializeContext()

let appPath = "./src/dailydos_api/" |> Path.getFullName
let projectPath = Path.combine appPath "dailydos_api.fsproj"

Target.create "Clean" ignore

Target.create "Restore" (fun _ ->
    DotNet.restore id projectPath
)

Target.create "Build" (fun _ ->
    DotNet.build id projectPath
)

Target.create "Run" (fun _ ->
  let server = async {
    DotNet.exec (fun p -> { p with WorkingDirectory = appPath } ) "watch" "run" |> ignore
  }

  server
  |> Async.RunSynchronously
  |> ignore
)

open Fake.Core.TargetOperators

let dependencies = [
    "Clean"
      ==> "Restore"
      ==> "Build"

    "Clean"
      ==> "Restore"
      ==> "Run"
]

let runOrDefault args =
    try
        match args with
        | [| target |] -> Target.runOrDefault target
        | _ -> Target.runOrDefault "Run"
        0
    with e ->
        printfn "%A" e
        1

[<EntryPoint>]
let main args = runOrDefault args
