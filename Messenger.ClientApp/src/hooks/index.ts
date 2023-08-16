import { useDispatch, useSelector } from 'react-redux'
import type { TypedUseSelectorHook } from 'react-redux'
import { AppDispatch, RootState } from '../store';

// Use throughout your app instead of plain `useDispatch` and `useSelector`
export const useAppDispatch: () => AppDispatch = useDispatch
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector

export { useConversation } from "./useConversation";
export { useRoutes } from "./useRoutes";
export { useActiveList } from "./useActiveList";
export { useOtherUser } from "./useOtherUser";
export { useSessionUser } from "./useSessionUser";