import {ActivityInput} from "@/generated/models";
import {RecurringType, validRecurringType} from "@/enums/RecurringType";

export function validateActivityInput(activityInput: ActivityInput): boolean {
    const recurring = activityInput.recurringType !== RecurringType.NO

    // duration makes sense?
    const minDurationValid = activityInput.minDuration >= 0;
    const maxDurationValid = activityInput.maxDuration === 0 || activityInput.maxDuration > activityInput.minDuration
    if (!minDurationValid || !maxDurationValid) {
        return false;
    }

    // weekdayConstraints only contains 0s and 1s?
    const rExpWeekday = /[^0-1]/g;
    const weekdayConstraintValid = !rExpWeekday.test(activityInput.weekdayConstraint) && activityInput.weekdayConstraint.length === 7;
    if (!weekdayConstraintValid) {
        return false;
    }

    // name only contains unicode-letters or spaces
    const rExpName = /[^\p{Letter}|\s]/gu;
    const nameValid = !rExpName.test(activityInput.name)
    if (!nameValid) {
        return false;
    }

    // check recurring type if there
    const recurringTypeValid = !recurring || (recurring && validRecurringType(activityInput.recurringType));
    if (!recurringTypeValid) {
        return false;
    }

    return !recurring || (activityInput.recurringInterval > 0 && activityInput.recurringInterval < 100)
}
