import { default as axios } from "lib/axios";
import GetUniversitiesResponse from "./DTOs/GET/GetUniversitiesResponse";

const universityApiBaseUrl = "https://localhost:7009";

export const getDestinations = async (page: number, pageSize: number): Promise<GetUniversitiesResponse[]> => {
  return await axios
    .get<GetUniversitiesResponse[]>(`${universityApiBaseUrl}/universities?pageSize=${pageSize}&page=${page}`)
    .then(response => response.data)
    .catch(error => error);
};
