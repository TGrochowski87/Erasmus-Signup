import { configureStore, ThunkAction, Action } from "@reduxjs/toolkit";
import universitySlice from "./universitySlice";
import loginSlice from "./loginSlice";

export const store = configureStore({
  reducer: {
    university: universitySlice,
    login: loginSlice,
  },
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;
