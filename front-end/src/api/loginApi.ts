import { default as axios } from "lib/axios";
import OAuthData from "models/OAuthData";

export const getAuthUrl = async (): Promise<OAuthData> => {
  return await axios
    .get<OAuthData>(
      "https://localhost:7077/oauth/oauth_url?callbackPath=" +
        encodeURIComponent(
          window.location.href.slice(window.location.origin.length + 1)
        )
    )
    .then((response) => response.data)
    .catch((error) => error);
};
