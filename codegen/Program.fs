namespace DailyDos.Codegen

open System.IO
open DailyDos.Codegen.ModelService

/// Contains everything to generate F# from the definitions
module FsGenerator =
    let generateModel model =
        [ "[<CLIMutable>]"
          $"type %s{model.name} = {{"
          for prop in model.properties do
              let typ =
                  if (prop.typ = GCustom) then
                      prop.customType
                  else
                      FsType.fromGenType prop.typ |> FsType.toFs

              $"    %s{prop.name}: %s{typ}"
          $"}}" ]

    let instance =
        { fileHeader = [ "namespace DailyDos.Generated \nopen System" ]
          outputPath = "./api/generated/models.fs"
          generateModel = generateModel }

/// Contains everything to generate TypeScript from the definitions
module TsGenerator =
    let generateModel model =
        [ $"export interface %s{model.name} {{"
          for prop in model.properties do
              let typ =
                  if (prop.typ = GCustom) then
                      prop.customType
                  else
                      TsType.fromGenType prop.typ |> TsType.toTs

              $"    %s{prop.name}: %s{typ};"
          $"}}" ]

    let instance =
        { fileHeader = []
          outputPath = "./vue_client/src/generated/models.ts"
          generateModel = generateModel }

/// Contains the entry point of the program
module Program =
    [<EntryPoint>]
    let main _ =
        let writeAllLines file (lines: string list) = File.WriteAllLines(file, lines)

        [ FsGenerator.instance
          TsGenerator.instance ]
        // Every generator has to generate everything
        |> List.iter (fun generator ->
            // Generate the file contents
            List.map generator.generateModel ModelDefinitions.get
            // Add the defined file header
            |> List.append [ generator.fileHeader ]
            |> List.reduce (fun l1 l2 -> [ yield! l1; ""; yield! l2 ])
            // Write it to disk now
            |> writeAllLines generator.outputPath)

        0 // return an integer exit code
