export enum RecurringType {
    NO = 'no', DAILY = 'daily', WEEKLY = 'weekly', MONTHLY = 'monthly'
}

// creates an array containing every recurringtype minus no
export function createAvailableRecurringTypes(): RecurringType[] {
    const recurringTypes = Object.values(RecurringType);
    return recurringTypes.map((value) => {
        return createRecurringTypeFromString(value);
    });
}

export function validRecurringType(type: string): boolean {
    switch (type) {
        case 'no':
        case 'daily':
        case 'weekly':
        case 'monthly':
            return true;

        default:
            return false;
    }
}

export function createRecurringTypeFromString(type: string): RecurringType {
    if (!validRecurringType(type)) {
        throw new TypeError('invalid recurring type!');
    }

    switch (type) {
        case 'no':
            return RecurringType.NO;
        case 'daily':
            return RecurringType.DAILY;
        case 'weekly':
            return RecurringType.WEEKLY;
        default:
            return RecurringType.MONTHLY;
    }
}
