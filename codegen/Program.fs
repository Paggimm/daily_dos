namespace DailyDos.Codegen

open System
open System.IO
open DailyDos.Codegen.ModelService

module FsGenerator =
    let generateModel model =
        [ "[<CLIMutable>]"
          $"type %s{model.name} = {{"
          for prop in model.properties do
              let typ =
                  FsType.fromGenType prop.typ |> FsType.toFs

              $"    %s{prop.name}: %s{typ}"
          $"}}" ]

module TsGenerator =
    let generateModel model =
        [ $"export interface %s{model.name} {{"
          for prop in model.properties do
              let typ =
                  TsType.fromGenType prop.typ |> TsType.toTs

              $"    %s{prop.name}: %s{typ};"
          $"}}" ]

module Program =
    [<EntryPoint>]
    let main _ =
        let writeAllLines file (lines: string list) = File.WriteAllLines(file, lines)

        let models = ModelService.getModels

        [ {| generate = FsGenerator.generateModel
             path = "../api/generated/models.fs"
             header = [ "namespace DailyDos.Generated" ] |}
          {| generate = TsGenerator.generateModel
             path = "../vue_client/src/generated/models.ts"
             header = [] |} ]
        |> List.iter
            (fun generator ->
                models
                |> List.map generator.generate
                |> List.append [ generator.header ]
                |> List.reduce (fun l1 l2 -> [ yield! l1; ""; yield! l2 ])
                |> writeAllLines generator.path)

        0 // return an integer exit code
