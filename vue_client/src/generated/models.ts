
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
