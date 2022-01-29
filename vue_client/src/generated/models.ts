
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
    duration: number;
    name: string;
}

export interface ActivityViewModel {
    user_id: number;
    duration: number;
    name: string;
}
