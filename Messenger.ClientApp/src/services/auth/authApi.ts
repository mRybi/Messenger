import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { AuthenticateUserCommand, RegisterUserCommand, UserInfo } from './authModels';
import { setUser } from '../../slices/userSlice';

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
        authenticate: builder.mutation<UserInfo, AuthenticateUserCommand>({
            query: (body) => ({
                url: "authenticate",
                method: 'POST',
                body,
            }),
            async onQueryStarted(args, { dispatch, queryFulfilled }) {
                try {
                    const { data } = await queryFulfilled;
                    dispatch(setUser(data));
                } catch (error) { }
            },
        }),
    }),
})

export const { useRegisterMutation, useAuthenticateMutation } = authApi;