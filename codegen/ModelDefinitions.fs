namespace DailyDos.Codegen.ModelService

open DailyDos.Codegen

/// Generates Modeldefinitions
/// When Generating new Definitions add them to -> get
module ModelDefinitions =
    let private LoginResponse =
        Model.create "LoginResponse"
        |> Model.withProperty "token" GString

    let private UserModel =
        Model.create "User"
        |> Model.withProperty "id" GInt
        |> Model.withProperty "name" GString

    let private RegisterModel =
        Model.create "RegisterData"
        |> Model.withProperty "name" GString
        |> Model.withProperty "email" GString
        |> Model.withProperty "password" GString

    let private LoginViewModel =
        Model.create "LoginViewModel"
        |> Model.withProperty "name" GString
        |> Model.withProperty "password" GString

    let private Activity =
        Model.create "Activity"
        |> Model.withProperty "id" GInt
        |> Model.withProperty "user_id" GInt
        |> Model.withProperty "duration" GInt
        |> Model.withProperty "name" GString

    let get =
        [ LoginResponse
          UserModel
          RegisterModel
          LoginViewModel
          Activity ]
