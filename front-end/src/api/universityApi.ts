import { default as axios } from "lib/axios";
import StudyArea from "models/StudyArea";
import StudyDomain from "models/StudyDomain";
import GetDestinationDetails from "./DTOs/GET/GetDestinationDetails";
import GetDestinations from "./DTOs/GET/GetDestinations";

const universityApiBaseUrl = "https://localhost:7009";

export const getDestinations = async (page: number, pageSize: number): Promise<GetDestinations> => {
  return await axios
    .get<GetDestinations>(`${universityApiBaseUrl}/universities?pageSize=${pageSize}&page=${page}`)
    .then(response => response.data)
    .catch(error => error);
};

export const getDestinationDetails = async (destinationId: number): Promise<GetDestinationDetails> => {
  return await axios
    .get<GetDestinationDetails>(`${universityApiBaseUrl}/universities/${destinationId}`)
    .then(response => response.data)
    .catch(error => error);
};

export const getStudyDomains = async (): Promise<StudyDomain[]> => {
  return await axios
    .get<StudyDomain>(`${universityApiBaseUrl}/study-domains`)
    .then(response => response.data)
    .catch(error => error);
};
