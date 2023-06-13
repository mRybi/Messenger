import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { UserInfo } from '../services/auth/authModels';

interface IUserState {
  user: UserInfo | null;
}

const initialState: IUserState = {
  user: null,
};

export const userSlice = createSlice({
  initialState,
  name: 'userSlice',
  reducers: {
    logout: () => initialState,
    setUser: (state, action: PayloadAction<UserInfo>) => {
      state.user = action.payload;
    },
  },
});

export const { logout, setUser } = userSlice.actions;