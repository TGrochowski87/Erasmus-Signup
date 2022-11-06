import { default as axios } from "lib/axios";
import University from "models/University";

export const getUniversities = async (): Promise<University[]> => {
  return await axios
    .get<University[]>("http://localhost:3000/universities")
    .then((response) => response.data)
    .catch((error) => error);
};
