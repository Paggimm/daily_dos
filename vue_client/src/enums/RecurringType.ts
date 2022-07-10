export enum RecurringType {
    DAILY = 'daily', WEEKLY = 'weekly', MONTHLY = 'monthly'
}

export function createAvailableRecurringTypes(): RecurringType[] {
    return Object.values(RecurringType);
}
