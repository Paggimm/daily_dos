namespace DailyDos.Validator.ActivityValidator

open System.Text.RegularExpressions
open DailyDos.Generated

module ActivityInputValidator =
    /// Validates if a given string only contains letters, numbers, underscore or space
    let private CheckInputString input : bool =
        let result = Regex("[^\\w|\\s]").Match(input)
        result.Success = false

    /// true, if duration 0 or both greater than 0 and min < max
    let private ValidateActivityDuration minDuration maxDuration =
        (minDuration = 0 && maxDuration = 0)
        || (minDuration > 0
            && maxDuration > 0
            && (minDuration < maxDuration))

    /// validates length and content of weekday_constraint
    let private ValidateActivityWeekdayConstraint (weekdayConstraint: string) =
        let mutable valid = true

        if String.length (weekdayConstraint) <> 7 then
            valid <- false

        String.iter
            (fun c ->
                match c with
                | '1'
                | '0' -> ()
                | _ -> valid <- false)
            weekdayConstraint

        valid

    /// validates a given ActivityViewModel
    let ValidateActivityInput (activity: Activity) : bool =
        let nameValid = CheckInputString activity.name

        let durationValid =
            ValidateActivityDuration activity.min_duration activity.max_duration

        let recurringTypeValid =
            match activity.recurring_type with
            | "daily"
            | "weekly"
            | "monthly" -> true
            | _ -> false

        let recurringIntervalValid = activity.recurring_interval >= 0


        let weekdayConstraintValid =
            ValidateActivityWeekdayConstraint activity.weekday_constraint

        nameValid
        && durationValid
        && recurringIntervalValid
        && recurringTypeValid
        && weekdayConstraintValid

    let ValidateActivityPatchInput (newActivity: Activity) (activity: Activity) user_id : bool =
        let ownerValid = activity.user_id = user_id
        let viewModelValid = ValidateActivityInput newActivity
        ownerValid && viewModelValid
