import { default as axios } from "lib/axios";
import StudyDomain from "models/StudyDomain";
import GetDestinationDetails from "./DTOs/GET/GetDestinationDetails";
import DestinationLists from "../models/DestinationList";

const universityApiBaseUrl = "https://localhost:7009";

export const getDestinations = async (page: number, pageSize: number): Promise<DestinationLists> => {
  return await axios
    .get<DestinationLists>(`${universityApiBaseUrl}/universities?pageSize=${pageSize}&page=${page}`)
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
