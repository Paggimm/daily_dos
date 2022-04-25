namespace DailyDos.Codegen

/// Type alias to clarify when a string is a line of a file
type Line = string

/// Base types to use in the generator
/// Will be mapped to types in the target languages
type GeneratorType =
    | GInt
    | GFloat
    | GString
    | GDate
    | GList of GeneratorType

/// Supported types in F#
type FsType =
    | FsInt
    | FsFloat
    | FsString
    | FsDate
    | FsList of FsType

/// Contains helper functions for FsType type
module FsType =
    /// Creates a FsType from given GeneratorType
    let fromGenType typ =
        let rec helper wrapper typ =
            match typ with
            | GInt -> wrapper FsInt
            | GFloat -> wrapper FsFloat
            | GString -> wrapper FsString
            | GList listType -> helper (wrapper >> FsList) listType
            | GDate -> wrapper FsDate

        helper id typ

    /// Gives back the string to be used in F# for given FsType
    let toFs typ =
        let rec helper wrapper typ =
            match typ with
            | FsInt -> "int"
            | FsFloat -> "float"
            | FsString -> "string"
            | FsList listType -> helper (wrapper >> (fun s -> s + " list")) listType
            | FsDate -> "DateTime"

        helper id typ

/// Supported types in TypeScript
type TsType =
    | TsNumber
    | TsString
    | TsDate
    | TsList of TsType

/// Contains helper functions for the TsType type
module TsType =
    /// Creates a TsType from given GeneratorType
    let fromGenType typ =
        let rec helper wrapper typ =
            match typ with
            | GInt -> wrapper TsNumber
            | GFloat -> wrapper TsNumber
            | GString -> wrapper TsString
            | GDate -> wrapper TsDate
            | GList listType -> helper (wrapper >> TsList) listType

        helper id typ

    /// Gives back the string to be used in TypeScript for given TsType
    let toTs typ =
        let rec helper wrapper typ =
            match typ with
            | TsNumber -> "number"
            | TsString -> "string"
            | TsDate -> "Date"
            | TsList listType -> helper (wrapper >> (fun s -> s + "[]")) listType

        helper id typ

/// A simple property of a model definition in the generator
type Property = { name: string; typ: GeneratorType }

/// A simple definition for a model to be generated
type Model =
    { name: string
      properties: Property list }

/// Contains helper methods for the Model type
module Model =
    /// Creates an empty model with given name
    let create name = { name = name; properties = [] }

    /// Returns a new model with given property added
    let withProperty name typ model =
        let property = { name = name; typ = typ }
        { model with properties = List.append model.properties [ property ] }

/// Everything you need to use a generator. Contains the generator itself and
/// stuff you need to configure
type Generator =
    { fileHeader: Line list
      outputPath: string
      generateModel: Model -> Line list }
