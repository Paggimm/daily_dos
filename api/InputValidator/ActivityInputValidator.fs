namespace DailyDos.Validator.ActivityValidator

open System.Text.RegularExpressions
open DailyDos.Generated

module ActivityInputValidator =
    /// Validates if a given string only contains letters, numbers, underscore or space
    let private CheckInputString input : bool =
        let result = Regex("[^\\p{L}\\s]").Match(input)
        result.Success = false

    /// true, if duration 0 or both greater than 0 and min < max
    let private ValidateActivityDuration minDuration maxDuration =
        let minDurationValid = minDuration >= 0
        let maxDurationValid = maxDuration = 0 || maxDuration > minDuration
        minDurationValid && maxDurationValid

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
    let ValidateActivityInput (activity: ActivityInput) : bool =
        let nameValid = CheckInputString activity.name

        let durationValid =
            ValidateActivityDuration activity.minDuration activity.maxDuration

        let recurringTypeValid =
            match activity.recurringType with
            | "daily"
            | "weekly"
            | "monthly" -> true
            | _ -> false

        let recurringIntervalValid = activity.recurringInterval >= 0

        let weekdayConstraintValid =
            ValidateActivityWeekdayConstraint activity.weekdayConstraint

        nameValid
        && durationValid
        && recurringIntervalValid
        && recurringTypeValid
        && weekdayConstraintValid

    let ValidateActivityPatchInput (newActivity: ActivityInput) (activity: Activity) userId : bool =
        let ownerValid = activity.userId = userId
        let viewModelValid = ValidateActivityInput newActivity
        ownerValid && viewModelValid
