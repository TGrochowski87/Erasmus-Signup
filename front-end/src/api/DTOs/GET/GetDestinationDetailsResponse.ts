import SpecialtyDetails from "models/SpecialtyDetails";

interface GetDestinationDetailsResponse {
  universityName: string;
  erasmusCode: string;
  country: string;
  city: string;
  flagUrl: string;
  link: string;
  email: string;
  selectedDestId: number;
  destinations: SpecialtyDetails[];
}

export default GetDestinationDetailsResponse;
