namespace DailyDos.Codegen.ModelService

open System
open DailyDos.Codegen

/// Generates Modeldefinitions
/// When Generating new Definitions add them to -> get
module ModelDefinitions =
    let private LoginResponse =
        Model.create "LoginResponse"
        |> Model.withProperty "token" GString ""

    let private UserModel =
        Model.create "User"
        |> Model.withProperty "id" GInt ""
        |> Model.withProperty "name" GString ""

    let private RegisterModel =
        Model.create "RegisterData"
        |> Model.withProperty "name" GString""
        |> Model.withProperty "email" GString ""
        |> Model.withProperty "password" GString ""

    let private LoginViewModel =
        Model.create "LoginViewModel"
        |> Model.withProperty "name" GString ""
        |> Model.withProperty "password" GString ""

    let private Activity =
        Model.create "Activity"
        |> Model.withProperty "id" GInt ""
        |> Model.withProperty "userId" GInt ""
        |> Model.withProperty "name" GString ""
        |> Model.withProperty "minDuration" GInt ""
        |> Model.withProperty "maxDuration" GInt ""
        |> Model.withProperty "weekdayConstraint" GString ""
        |> Model.withProperty "recurringType" GString ""
        |> Model.withProperty "recurringInterval" GInt ""
        |> Model.withProperty "createTime" GDate ""

    /// input model for post requests
    let private ActivityInput =
        Model.create "ActivityInput"
        |> Model.withProperty "name" GString ""
        |> Model.withProperty "minDuration" GInt ""
        |> Model.withProperty "maxDuration" GInt ""
        |> Model.withProperty "weekdayConstraint" GString ""
        |> Model.withProperty "recurringType" GString ""
        |> Model.withProperty "recurringInterval" GInt ""

    let private Plan =
        Model.create "Plan"
        |> Model.withProperty "id" GInt ""
        |> Model.withProperty "userId" GInt ""
        |> Model.withProperty "activity" GCustom Activity.name
        |> Model.withProperty "duration" GInt ""
        |> Model.withProperty "date" GDate ""
        |> Model.withProperty "repeatable" GString ""
        |> Model.withProperty "createTime" GDate ""

    /// input model for post requests
    let private PlanInput =
        Model.create "PlanInput"
        |> Model.withProperty "activityId" GInt ""
        |> Model.withProperty "duration" GInt ""
        |> Model.withProperty "date" GDate ""
        |> Model.withProperty "repeatable" GString ""

    let get = [
          LoginResponse
          UserModel
          RegisterModel
          LoginViewModel
          Activity
          ActivityInput
          Plan
          PlanInput
    ]
