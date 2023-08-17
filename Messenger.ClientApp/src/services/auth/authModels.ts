export type RegisterUserCommand = {
    name: string;
    password: string;
    email: string;
}

export type GetAllUsersResponse = {
    users: UserInfo[];
}

export type AuthenticateUserCommand = Omit<RegisterUserCommand, "name">;

export type UserInfo = Omit<RegisterUserCommand, "password"> & { token: string, id: string, image: string};