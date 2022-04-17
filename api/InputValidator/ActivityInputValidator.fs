namespace DailyDos.Validator.ActivityValidator

open System.Text.RegularExpressions
open DailyDos.Generated

module ActivityInputValidator =
    /// Validates if a given string only contains letters, numbers, underscore or space
    let private check_input_string input : bool =
        let result = Regex("[^\\w|\\s]").Match(input)
        result.Success = false

    /// true, if duration 0 or both greater than 0 and min < max
    let private validate_acitivity_duration min_duration max_duration =
        (min_duration = 0 && max_duration = 0)
        || (min_duration > 0
            && max_duration > 0
            && (min_duration < max_duration))

    /// validates length and content of weekday_constraint
    let private validate_activity_weekday_constraint (weekday_constraint: string) =
        let mutable valid = true

        if String.length (weekday_constraint) <> 7 then
            valid <- false

        String.iter
            (fun c ->
                match c with
                | '1'
                | '0' -> ()
                | _ -> valid <- false)
            weekday_constraint

        valid

    /// validates a given ActivityViewModel
    let validate_actitivy_input (activity_view_model: ActivityViewModel) : bool =
        let name_valid = check_input_string activity_view_model.name

        let duration_valid =
            validate_acitivity_duration activity_view_model.min_duration activity_view_model.max_duration

        let recurring_type_valid =
            match activity_view_model.recurring_type with
            | "daily"
            | "weekly"
            | "monthly" -> true
            | _ -> false

        let recurring_interval_valid = activity_view_model.recurring_interval >= 0


        let weekday_constraint_valid =
            validate_activity_weekday_constraint activity_view_model.weekday_constraint

        name_valid
        && duration_valid
        && recurring_interval_valid
        && recurring_type_valid
        && weekday_constraint_valid

    let validate_activity_patch_input (activity_view_model: ActivityViewModel) (activity: Activity) user_id : bool =
        let owner_valid = activity.user_id = user_id
        let view_model_valid = validate_actitivy_input activity_view_model
        owner_valid && view_model_valid
