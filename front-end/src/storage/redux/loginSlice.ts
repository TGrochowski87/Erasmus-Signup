import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getAuthUrl } from "api/oAuthUrlApi";
import RequestStatus from "./RequestStatus";

interface State {
  userLoggedIn: boolean;
  status: RequestStatus;
}

const initialState: State = {
  userLoggedIn: false,
  status: RequestStatus.idle,
};

export const logIn = createAsyncThunk("login", async () => {
  //TODO: Add error handling
  getAuthUrl().then((response) => {
    window.location.href = response;
  });
});

const loginSlice = createSlice({
  name: "oauth_auth_url",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(logIn.pending, (state) => {
        state.status = RequestStatus.loading;
      })
      .addCase(logIn.fulfilled, (state, action) => {
        state.status = RequestStatus.idle;
        //console.log(action.payload);
        //state.value = action.payload;
      })
      .addCase(logIn.rejected, (state) => {
        state.status = RequestStatus.failed;
      });
  },
});

export default loginSlice.reducer;
