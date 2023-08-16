import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { AuthenticateUserCommand, GetAllUsersResponse, RegisterUserCommand, UserInfo } from './authModels';
import { setUser } from '../../slices/userSlice';
import { RootState } from '../../store';

export const authApi = createApi({
    reducerPath: 'authApi', //https://localhost:44390/User/register
    baseQuery: fetchBaseQuery({ baseUrl: `https://localhost:44390/api/User/`,
    prepareHeaders: (headers, { getState }) => {
        const token = (getState() as RootState).userState.user?.token
        const sessionUser = sessionStorage.getItem("user");
        const user = sessionUser ? JSON.parse(sessionUser) : null;
console.log("authApi", token, user)
        if (token) {
          headers.set('authorization', `Bearer ${token}`)
        } else if(user) {
          headers.set('authorization', `Bearer ${user.token}`)
        }
    
        return headers
    }}),
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
                    console.log("first login data", data)
                    sessionStorage.setItem('user', JSON.stringify(data))
                    dispatch(setUser(data));
                } catch (error) { }
            },
        }),
        logout: builder.mutation<void, void>({
            query: () => ({
                url: "logout",
                method: 'POST',
                body: {}
            })
        }),
        getAllUsers: builder.query<GetAllUsersResponse, void>({
            query: () => ({ url: "getAllUsers" }),
        }),
    }),
})

export const { useRegisterMutation, useAuthenticateMutation, useLogoutMutation, useGetAllUsersQuery } = authApi;