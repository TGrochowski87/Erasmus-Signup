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

/* CHANGES FROM SEBA
 
 const LoginPageContainer = () => {
  
  useEffect(() => {
    let paramString = window.location.toString().split('?')[1];
    let queryString = new URLSearchParams(paramString);
    if( queryString.has("oauth_token") && queryString.has("oauth_verifier")){

      let loginCallbackPath : string | null = queryString.get("loginCallbackPath");
      let oauth_token : string | null = queryString.get("oauth_token");
      let oauth_verifier : string | null = queryString.get("oauth_verifier");
      let date = new Date();
      date.setTime(date.getTime()+(2*60*60*1000));
      setCookie('oauth_token',oauth_token, { expires: date })
      setCookie('oauth_verifier', oauth_verifier, { expires: date })
      putLogin(
        oauth_token != null ? oauth_token.toString() : "",
        oauth_verifier != null ? oauth_verifier.toString() : "")
        .then(() => {
          window.location.assign(loginCallbackPath != null && loginCallbackPath.trim() != "" ? loginCallbackPath : "/");
        }
      );
    }
    else{
      getAuthUrl().then((value? : OAuthUrl | undefined) => {
        window.location.assign(value?.oAuthUrl != null ? value?.oAuthUrl : "");
      });
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  
 */
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
