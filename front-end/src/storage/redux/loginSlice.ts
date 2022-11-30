import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import AccessTokenRequestDto from "api/DTOs/AccessTokenRequestDto";
import { getAccessToken, getOAuthUrl } from "api/userApi";
import AccessTokenData from "models/AccessTokenData";
import OAuthData from "models/OAuthData";
import RequestStatus from "./RequestStatus";

interface State {
  userLoggedIn: boolean;
  status: RequestStatus;
}

const initialState: State = {
  userLoggedIn: false,
  status: RequestStatus.idle,
};

export const fetchOAuthUrl = createAsyncThunk<OAuthData>(
  "oauth_url",
  async () => {
    //TODO: Add error handling
    const response = await getOAuthUrl();
    return response;
  }
);

export const logIn = createAsyncThunk<
  AccessTokenData,
  { oAuthToken: string; oAuthVerifier: string }
>("login", async ({ oAuthToken, oAuthVerifier }) => {
  const oAuthTokenSecret = sessionStorage.getItem("token-secret");
  if (oAuthTokenSecret === null) {
    throw Error("OAuth Token missing.");
  }

  const response = await getAccessToken({
    oAuthToken,
    oAuthVerifier,
    oAuthTokenSecret,
  });
  return response;
});

const loginSlice = createSlice({
  name: "oauth_auth_url",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchOAuthUrl.pending, (state) => {
        state.status = RequestStatus.loading;
      })
      .addCase(fetchOAuthUrl.fulfilled, (state, action) => {
        state.status = RequestStatus.idle;
        sessionStorage.setItem("token-secret", action.payload.oAuthTokenSecret);
        window.location.href = action.payload.oAuthUrl;
      })
      .addCase(fetchOAuthUrl.rejected, (state) => {
        state.status = RequestStatus.failed;
      })
      .addCase(logIn.pending, (state) => {
        state.status = RequestStatus.loading;
      })
      .addCase(logIn.fulfilled, (state, action) => {
        state.status = RequestStatus.idle;
        sessionStorage.removeItem("token-secret");
        sessionStorage.setItem("access-token", action.payload.accessToken);
        sessionStorage.setItem(
          "access-token-secret",
          action.payload.accessTokenSecret
        );
        state.userLoggedIn = true;
      })
      .addCase(logIn.rejected, (state) => {
        state.status = RequestStatus.failed;
      });
  },
});

export default loginSlice.reducer;
