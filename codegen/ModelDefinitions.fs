namespace DailyDos.Codegen.ModelService

open DailyDos.Codegen

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
        |> Model.withProperty "username" GString
        |> Model.withProperty "email" GString
        |> Model.withProperty "password" GString

    let get =
        [ LoginResponse
          UserModel
          RegisterModel ]
