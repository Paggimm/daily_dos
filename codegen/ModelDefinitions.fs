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
        |> Model.withProperty "userId" GInt
        |> Model.withProperty "name" GString
        |> Model.withProperty "minDuration" GInt
        |> Model.withProperty "maxDuration" GInt
        |> Model.withProperty "weekdayConstraint" GString
        |> Model.withProperty "recurringType" GString
        |> Model.withProperty "recurringInterval" GInt
        |> Model.withProperty "createTime" GDate

    let private ActivityInput =
        Model.create "ActivityInput"
        |> Model.withProperty "name" GString
        |> Model.withProperty "minDuration" GInt
        |> Model.withProperty "maxDuration" GInt
        |> Model.withProperty "weekdayConstraint" GString
        |> Model.withProperty "recurringType" GString
        |> Model.withProperty "recurringInterval" GInt

    let get =
        [ LoginResponse
          UserModel
          RegisterModel
          LoginViewModel
          Activity
          ActivityInput ]
