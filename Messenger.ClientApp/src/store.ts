import { configureStore } from '@reduxjs/toolkit'
import { authApi, conversationApi } from "./services"
import { userSlice } from './slices/userSlice'

export const store = configureStore({
  reducer: {
    [conversationApi.reducerPath]: conversationApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
    userState: userSlice.reducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(authApi.middleware).concat(conversationApi.middleware),
  
  // [authApi.middleware, conversationApi.middleware] as const,
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch