import {createRecurringTypeFromString, RecurringType, validRecurringType} from "../enums/RecurringType";

test("test a valid RecurringType-string", () => {
    expect(validRecurringType('daily')).toBeTruthy();
})

test("test an invalid RecurringType-string", () => {
    expect(validRecurringType('dialy')).toBeFalsy();
})

test("create RecurringType from string", () => {
    expect(createRecurringTypeFromString('daily')).toBe(RecurringType.DAILY);
})

test("fail to create RecurringType from string", () => {
    expect(() => {
        createRecurringTypeFromString('dialy');
    }).toThrow(TypeError);
})
