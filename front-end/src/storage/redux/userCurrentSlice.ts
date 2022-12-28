import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getCurrentUserData } from "api/userApi";
import { User } from "models/User";
import RequestStatus from "./RequestStatus";

interface State {
  value: User | undefined;
  status: RequestStatus;
}

const initialState: State = {
  value: undefined,
  status: RequestStatus.idle,
};

export const fetchUserCurrent = createAsyncThunk("userCurrent", async () => {
  //TODO: Add error handling
  const response = await getCurrentUserData();
  return response;
});

const userCurrentSlice = createSlice({
  name: "userCurrent",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(fetchUserCurrent.pending, state => {
        state.status = RequestStatus.loading;
      })
      .addCase(fetchUserCurrent.fulfilled, (state, action) => {
        state.status = RequestStatus.idle;
        state.value = action.payload;
      })
      .addCase(fetchUserCurrent.rejected, state => {
        state.status = RequestStatus.failed;
      });
  },
});

export default userCurrentSlice.reducer;
