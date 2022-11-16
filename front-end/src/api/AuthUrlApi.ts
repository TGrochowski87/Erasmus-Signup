import { default as axios } from "lib/axios";
import OAuthUrl from "models/OAuthUrl";

export const getAuthUrl = async (): Promise<OAuthUrl> => {
  return await axios
    .get<OAuthUrl>(
      "https://localhost:7077/user/oauth_url?callbackPath=" +
      encodeURIComponent(window.location.href.slice(window.location.origin.length+1)))
    .then((response) => response.data)
    .catch((error) => error);
};
