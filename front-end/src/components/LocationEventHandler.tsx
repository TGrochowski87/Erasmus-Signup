import { useEffect } from "react";
import { useLocation, useSearchParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "storage/redux/hooks";
import { logIn, logOut, logOutLocally } from "storage/redux/userSlice";
import { RootState } from "storage/redux/store";
import decodeJwt from "utilities/decodeJwt";

const LocationEventHandler = () => {
  const { userLoggedIn } = useAppSelector((state: RootState) => state.login);
  const dispatch = useAppDispatch();
  const location = useLocation();
  const [searchParams, setSearchParams] = useSearchParams();

  // Handles OAuth callback
  useEffect(() => {
    const oAuthToken = searchParams.get("oauth_token");
    const oAuthVerifier = searchParams.get("oauth_verifier");
    searchParams.delete("oauth_token");
    searchParams.delete("oauth_verifier");
    setSearchParams(searchParams);

    if (oAuthToken && oAuthVerifier) {
      dispatch(logIn({ oAuthToken, oAuthVerifier }));
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [searchParams]);

  // Handles monitoring JWT expiry
  useEffect(() => {
    const accessToken = localStorage.getItem("access-token");
    if (accessToken === null) {
      if (userLoggedIn) {
        dispatch(logOutLocally());
      }
      return;
    }

    const tokenPayload = decodeJwt(accessToken);
    if (Date.now() > tokenPayload.exp * 1000) {
      dispatch(logOut());
      dispatch(logOutLocally());
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [location]);

  return <></>;
};

export default LocationEventHandler;
