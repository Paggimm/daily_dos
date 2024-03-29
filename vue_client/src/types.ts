import type {RecurringType} from "@/enums/RecurringType";

export interface IRecurringInput {
    recurringType: RecurringType,
    recurringInterval: number
}
