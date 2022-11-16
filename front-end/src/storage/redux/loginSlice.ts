import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getAuthUrl } from "api/AuthUrlApi";
import OAuthUrl from "models/OAuthUrl";
import RequestStatus from "./RequestStatus";

interface State {
  value: OAuthUrl | null;
  status: RequestStatus;
}

const initialState: State = {
  value : null,
  status: RequestStatus.idle,
};

export const fetchOauthUrl = createAsyncThunk("oauth_url", async () => {
  //TODO: Add error handling
  const response = await getAuthUrl();
  console.log("resp", response);
  return response;
});

const loginSlice = createSlice({
  name: "oauth_auth_url",
  initialState,
  reducers: {
    test: (state) => {
      state.value = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchOauthUrl.pending, (state) => {
        state.status = RequestStatus.loading;
        console.log("state pend",state);

      })
      .addCase(fetchOauthUrl.fulfilled, (state, action) => {
        state.status = RequestStatus.idle;
        state.value = action.payload;
        console.log("state ful",state,action.payload);
      })
      .addCase(fetchOauthUrl.rejected, (state) => {
        state.status = RequestStatus.failed;
        console.log("state rej",state);

      });
  },
});

export default loginSlice.reducer;
