import DestSpecialty from "models/DestSpecialty";

interface GetDestinationsResponse {
  totalRows: number;
  destinations: DestSpecialty[];
}

export default GetDestinationsResponse;
