
export interface LoginResponse {
    token: string;
}

export interface User {
    id: number;
    name: string;
}

export interface RegisterData {
    username: string;
    email: string;
    password: string;
}
