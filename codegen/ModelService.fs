namespace DailyDos.Codegen.ModelService

open DailyDos.Codegen

module ModelService =
    // Model LoginResponse
    let private LoginResponse =
        Model.create "LoginResponse"
        |> Model.withProperty { name = "token"; typ = GString }

    // Model User
    let private UserModel =
        Model.create "User"
        |> Model.withProperty { name = "id"; typ = GInt }
        |> Model.withProperty { name = "name"; typ = GString }

    // Register Model
    let private RegisterModel =
        Model.create "RegisterData"
        |> Model.withProperty { name = "name"; typ = GString }
        |> Model.withProperty { name = "email"; typ = GString }
        |> Model.withProperty { name = "password"; typ = GString }

    let getModels =
        [ LoginResponse
          UserModel
          RegisterModel ]
