import DestSpecialty from "models/DestSpecialty";

interface GetDestinations {
  totalRows: number;
  destinations: DestSpecialty[];
}

export default GetDestinations;
