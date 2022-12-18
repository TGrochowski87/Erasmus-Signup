import { default as axios } from "lib/axios";
import GetDestinationDetailsResponse from "./DTOs/GET/GetDestinationDetailsResponse";
import GetDestinationsResponse from "./DTOs/GET/GetDestinationsResponse";

const universityApiBaseUrl = "https://localhost:7009";

export const getDestinations = async (page: number, pageSize: number): Promise<GetDestinationsResponse> => {
  return await axios
    .get<GetDestinationsResponse>(`${universityApiBaseUrl}/universities?pageSize=${pageSize}&page=${page}`)
    .then(response => response.data)
    .catch(error => error);
};

export const getDestinationDetails = async (destinationId: number): Promise<GetDestinationDetailsResponse> => {
  return await axios
    .get<GetDestinationDetailsResponse>(`${universityApiBaseUrl}/universities/${destinationId}`)
    .then(response => response.data)
    .catch(error => error);
};
