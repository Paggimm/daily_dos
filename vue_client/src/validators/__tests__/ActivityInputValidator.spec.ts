import {RecurringType} from "@/enums/RecurringType";
import { describe, it, expect } from 'vitest'
import { validateActivityInput } from "../ActivityInputValidator";

it("test a correct ActivityInput", () => {
    expect(validateActivityInput({
        name: "těst",
        weekdayConstraint: "0000000",
        minDuration: 10,
        maxDuration: 50,
        recurringInterval: 2,
        recurringType: RecurringType.DAILY
    })).toBeTruthy();
})

it("test activityInput with incorrect name", () => {
    expect(validateActivityInput({
        name: "těst;",
        weekdayConstraint: "0000000",
        minDuration: 10,
        maxDuration: 50,
        recurringInterval: 2,
        recurringType: RecurringType.DAILY
    })).toBeFalsy();
})

it("test activityInput with incorrect weekday", () => {
    expect(validateActivityInput({
        name: "těst",
        weekdayConstraint: "0000002",
        minDuration: 10,
        maxDuration: 50,
        recurringInterval: 2,
        recurringType: RecurringType.DAILY
    })).toBeFalsy();
})

it("test activityInput with to low minDuration", () => {
    expect(validateActivityInput({
        name: "těst",
        weekdayConstraint: "0000001",
        minDuration: -1,
        maxDuration: 50,
        recurringInterval: 2,
        recurringType: RecurringType.DAILY
    })).toBeFalsy();
})

it("test activityInput with no duration set", () => {
    expect(validateActivityInput({
        name: "těst",
        weekdayConstraint: "0000001",
        minDuration: 0,
        maxDuration: 0,
        recurringInterval: 2,
        recurringType: RecurringType.DAILY
    })).toBeTruthy();
})

it("test activityInput with invalid RecurringType", () => {
    expect(validateActivityInput({
        name: "těst",
        weekdayConstraint: "0000001",
        minDuration: 5,
        maxDuration: 6,
        recurringInterval: 2,
        recurringType: 'DIALY'
    })).toBeFalsy();
})

it("test activityInput with invalid RecurringInterval", () => {
    expect(validateActivityInput({
        name: "těst",
        weekdayConstraint: "0000001",
        minDuration: 5,
        maxDuration: 6,
        recurringInterval: -1,
        recurringType: RecurringType.DAILY
    })).toBeFalsy();
})
