import UserSpecialty from "./UserSpecialty";

interface User {
  firstName: string;
  lastName: string;
  index: string;
  specialties: UserSpecialty[];
}

export default User;
