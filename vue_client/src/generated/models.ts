
export interface LoginResponse {
    token: string;
}

export interface User {
    id: number;
    name: string;
}

export interface RegisterData {
    name: string;
    email: string;
    password: string;
}

export interface LoginViewModel {
    name: string;
    password: string;
}

export interface Activity {
    id: number;
    userId: number;
    name: string;
    minDuration: number;
    maxDuration: number;
    weekdayConstraint: string;
    recurringType: string;
    recurringInterval: number;
    createTime: Date;
}

export interface ActivityInput {
    name: string;
    minDuration: number;
    maxDuration: number;
    weekdayConstraint: string;
    recurringType: string;
    recurringInterval: number;
}

export interface Plan {
    id: number;
    userId: number;
    activity: Activity;
    duration: number;
    date: Date;
    repeatable: string;
    createTime: Date;
}

export interface PlanInput {
    activityId: number;
    duration: number;
    date: Date;
    repeatable: string;
}

export interface PlanRating {
    id: number;
    planId: number;
    rating: number;
    isPreRating: boolean;
    createTime: Date;
}

export interface PlanRatingInput {
    planId: number;
    rating: number;
    isPreRating: boolean;
}

export interface FreeTime {
    id: number;
    userId: number;
    startDate: Date;
    duration: number;
    recurringType: string;
    recurringInterval: string;
    createTime: Date;
}

export interface FreeTimeInput {
    startDate: Date;
    duration: number;
    recurringType: string;
    recurringInterval: string;
}
