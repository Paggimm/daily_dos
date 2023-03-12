import {createRecurringTypeFromString, RecurringType, validRecurringType} from "@/enums/RecurringType";
import { describe, it, expect } from 'vitest'

it("test a valid RecurringType-string", () => {
    expect(validRecurringType('daily')).toBeTruthy();
})

it("test an invalid RecurringType-string", () => {
    expect(validRecurringType('dialy')).toBeFalsy();
})

it("create RecurringType from string", () => {
    expect(createRecurringTypeFromString('daily')).toBe(RecurringType.DAILY);
})

it("fail to create RecurringType from string", () => {
    expect(() => {
        createRecurringTypeFromString('dialy');
    }).toThrow(TypeError);
})
