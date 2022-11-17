// Redux
import { useAppDispatch, useAppSelector } from "storage/redux/hooks";
import { RootState } from "storage/redux/store";
import { useEffect } from "react";
import { fetchOauthUrl } from "storage/redux/loginSlice";
// Components
import LoginPage from "./LoginPage";
// import {getAuthUrl} from "api/AuthUrlApi";
// import OAuthUrl from "models/OAuthUrl";
// import { redirect, Router, useNavigate } from "react-router-dom";
// import { getCookie, setCookie } from 'typescript-cookie'

const LoginPageContainer = () => {
  // let paramString = window.location.toString().split('?')[1];
  // let queryString = new URLSearchParams(paramString);

  // console.log(queryString.get("oauth_token"), queryString.get("oauth_verifier"));

  // if( queryString.has("oauth_token") && queryString.has("oauth_verifier")){
  //   console.log("has");
  //   let loginCallbackPath : string | null = queryString.get("loginCallbackPath");
  //   setCookie('oauth_token', queryString.get("oauth_token"), { expires: 7 })
  //   setCookie('oauth_verifier', queryString.get("oauth_verifier"), { expires: 7 })
  //   // TODO : POST login to userAPI
  //   window.location.assign(loginCallbackPath != null ? loginCallbackPath : "/");
  // }
  // else{
  //   getAuthUrl().then((value? : OAuthUrl | undefined) => {
  //     console.log("vs", value, value?.oAuthUrl);
  //     window.location.assign(value?.oAuthUrl != null ? value?.oAuthUrl : "");
  //   });
  // }

  const oauth_url = useAppSelector(
    (state: RootState) => state.login.value?.oAuthUrl
  );
  const dispatch = useAppDispatch();
  console.log(oauth_url);

  useEffect(() => {
    if (oauth_url === null) {
      dispatch(fetchOauthUrl());
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  console.log("after effects", oauth_url);
  return <LoginPage />;
};

export default LoginPageContainer;
