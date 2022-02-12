
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
    user_id: number;
    name: string;
    min_duration: number;
    max_duration: number;
    weekday_constraint: number;
    recurring_type: string;
    recurring_interval: number;
    create_time: number;
}

export interface ActivityViewModel {
    user_id: number;
    name: string;
    min_duration: number;
    max_duration: number;
    weekday_constraint: number;
    recurring_type: string;
    recurring_interval: number;
    create_time: number;
}
