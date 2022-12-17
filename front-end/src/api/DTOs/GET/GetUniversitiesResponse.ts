import DestSpecialty from "models/DestSpecialty";

interface GetUniversitiesResponse {
  totalRows: number;
  destinations: DestSpecialty[];
}

export default GetUniversitiesResponse;
