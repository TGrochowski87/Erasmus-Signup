import DestSpecialty from "models/DestSpecialty";

interface DestinationLists {
  totalRows: number;
  destinations: DestSpecialty[];
  recomendedDestinations: DestSpecialty[];
  recommendedByStudentsDestinations: DestSpecialty[];
}

export default DestinationLists;
