
export interface LoginResponse {
    token: string;
}

export interface User {
    id: number;
    name: string;
}

export interface Register {
    name: string;
    email: string;
    password: string;
}
