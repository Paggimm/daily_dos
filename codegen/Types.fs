namespace DailyDos.Codegen

type GeneratorType =
    | GInt
    | GFloat
    | GString
    | GList of GeneratorType

type FsType =
    | FsInt
    | FsFloat
    | FsString
    | FsList of FsType

module FsType =
    let fromGenType typ =
        let rec helper wrapper typ =
            match typ with
            | GInt -> wrapper FsInt
            | GFloat -> wrapper FsFloat
            | GString -> wrapper FsString
            | GList listType -> helper (wrapper >> FsList) listType

        helper id typ

    let toFs typ =
        let rec helper wrapper typ =
            match typ with
            | FsInt -> "int"
            | FsFloat -> "float"
            | FsString -> "string"
            | FsList listType -> helper (wrapper >> (fun s -> s + " list")) listType

        helper id typ

type TsType =
    | TsNumber
    | TsString
    | TsList of TsType

module TsType =
    let fromGenType typ =
        let rec helper wrapper typ =
            match typ with
            | GInt -> wrapper TsNumber
            | GFloat -> wrapper TsNumber
            | GString -> wrapper TsString
            | GList listType -> helper (wrapper >> TsList) listType

        helper id typ

    let toTs typ =
        let rec helper wrapper typ =
            match typ with
            | TsNumber -> "number"
            | TsString -> "string"
            | TsList listType -> helper (wrapper >> (fun s -> s + "[]")) listType

        helper id typ

type Property = { name: string; typ: GeneratorType }

type Model =
    { name: string
      properties: Property list }

module Model =
    let create name = { name = name; properties = [] }

    let withProperty property model =
        { model with
              properties = List.append model.properties [ property ] }
