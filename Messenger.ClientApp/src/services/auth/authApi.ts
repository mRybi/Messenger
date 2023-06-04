import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { RegisterUserCommand } from './authModels';

export const authApi = createApi({
    reducerPath: 'authApi', //https://localhost:44390/User/register
    baseQuery: fetchBaseQuery({ baseUrl: `https://localhost:44390/api/User/` }),
    endpoints: (builder) => ({
        register: builder.mutation<string, RegisterUserCommand>({
            query: (body) => ({
                url: "register",
                method: 'POST',
                body,
            }),
        }),
    }),
})

export const { useRegisterMutation } = authApi;