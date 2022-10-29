namespace DailyDos.Api.InputValidator.PlanInputValidator

open DailyDos.Generated
open DailyDos.Api.Daos.ActivityDao
open System

module PlanInputValidator =
    /// validates if acitivity exists and is owned by user
    let private ValidateActivityId id =
        let result =
            ActivityDao.DoesActivityExist id

        if not result then
            printf "activity does not exist"

        result

    /// validate duration constraints
    let private ValidateDuration duration =
        // duration has to be greater than zero and lower than a day
        if duration < 1 && duration > 1440 then
            printf "duration is lower 1 or greater than a day"
            false
        else
            true

    /// validate Date constraints
    let private ValidateDate (date: DateTime) =
        // is the date in the future?
        if date <= DateTime.UtcNow then
            printf "date is not in the future"
            false
        else
            true

    /// validates the repeatable constraint
    let private ValidateRepeatable repeatable =
        if repeatable = "daily"
           || repeatable = "weekly"
           || repeatable = "monthly"
           || repeatable = "yearly"
           || repeatable = "no" then
            true
        else
            false

    let ValidatePlanInput (planInput: PlanInput) =
        ValidateActivityId planInput.activityId
        && ValidateDuration planInput.duration
        && ValidateDate planInput.date
        && ValidateRepeatable planInput.repeatable
