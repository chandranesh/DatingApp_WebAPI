export type User = {
    id: number;
    displayName: string;
    email: string;
    token: string;
    imageUrl?: string;
}

export type LoginCredentials = {
    email: string;
    password: string;
}

export type RegisterCredentials = {    
    email: string;
    displayName: string;
    password: string;
}