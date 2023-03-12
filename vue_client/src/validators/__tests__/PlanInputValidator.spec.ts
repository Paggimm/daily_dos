import {ValidatePlanInput} from "@/validators/PlanInputValidator";
import { describe, it, expect } from 'vitest'

it("test a correct planInput", () => {
    expect(ValidatePlanInput({
        activityId: 1,
        duration: 60,
        repeatable: "no",
        date: new Date(2099, 1, 1)
    })).toBeTruthy();
});

it("test planInput with incorrect activityId", () => {
    expect(ValidatePlanInput({
        activityId: 0,
        duration: 60,
        repeatable: "no",
        date: new Date(2099, 1, 1)
    })).toBeFalsy()
});

it("test planInput with duration lower than 0", () => {
    expect(ValidatePlanInput({
        activityId: 1,
        duration: -5,
        repeatable: "no",
        date: new Date(2099, 1, 1)
    })).toBeFalsy()
});

it("test planInput with duration 0", () => {
    expect(ValidatePlanInput({
        activityId: 1,
        duration: 0,
        repeatable: "no",
        date: new Date(2099, 1, 1)
    })).toBeFalsy()
});

it("test planInput with invalid recurringType", () => {
    expect(ValidatePlanInput({
        activityId: 1,
        duration: 60,
        repeatable: "gepöniert",
        date: new Date(2099, 1, 1)
    })).toBeFalsy()
});

it("test planInput with date in the past", () => {
    expect(ValidatePlanInput({
        activityId: 1,
        duration: 60,
        repeatable: "no",
        date: new Date(1980, 1, 1)
    })).toBeFalsy()
});
