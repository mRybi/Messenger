import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { RootState } from '../../store';
import { Conversation, CreateConversationCommand } from './conversationModels';

export const conversationApi = createApi({
    reducerPath: 'conversationApi',
    baseQuery: fetchBaseQuery({ baseUrl: `https://localhost:44390/api/Conversation/`,
    prepareHeaders: (headers, { getState }) => {
        const token = (getState() as RootState).userState.user?.token
        const sessionUser = sessionStorage.getItem("user");
        const user = sessionUser ? JSON.parse(sessionUser) : null;

        if (token) {
          headers.set('authorization', `Bearer ${token}`)
        } else if(user) {
          headers.set('authorization', `Bearer ${user.token}`)
        }
        return headers
    }}),
    endpoints: (builder) => ({
        createConversation: builder.mutation<string, CreateConversationCommand>({
            query: (body) => ({
                url: "create",
                method: 'POST',
                body,
            }),
        }),
        getConversationForUser: builder.query<Conversation[], string>({
            query: (userId) => ({ url: `getByUserId?Id=${userId}` }),
        }),
    }),
})

export const { useCreateConversationMutation, useGetConversationForUserQuery } = conversationApi;