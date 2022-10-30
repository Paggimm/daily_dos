import {PlanInput} from "@/generated/models";
import {validRecurringType} from "@/enums/RecurringType";

export function ValidatePlanInput(planInput: PlanInput): boolean {
    if (planInput.activityId <= 0) {
        console.log("invalid activityId")
        return false;
    }

    if (planInput.date < new Date()) {
        console.log(`invalid date`);
        return false;
    }

    if (!validRecurringType(planInput.repeatable)) {
        console.log("invalid recurringType")
        return false;
    }

    // a plan cannot be longer than a day
    // TODO: overthink this, should this be actually the case?
    if (planInput.duration <= 0 || planInput.duration > 86400) {
        console.log("invalid duration")
        return false;
    }

    return true;
}
