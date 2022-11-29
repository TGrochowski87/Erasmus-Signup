import { default as axios } from "lib/axios";

export const getAuthUrl = async (): Promise<string> => {
  return await axios
    .get<string>(
      "https://localhost:7077/user/oauth_url?callbackPath=" +
        encodeURIComponent(
          window.location.href.slice(window.location.origin.length + 1)
        )
    )
    .then((response) => response.data)
    .catch((error) => error);
};
