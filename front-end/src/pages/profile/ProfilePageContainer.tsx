import User from "models/User";
import { useState } from "react";
import ProfilePage from "./ProfilePage";

const ProfilePageContainer = () => {
  const [userData, setUserData] = useState<User>({
    firstName: "Test",
    lastName: "Testowy",
    index: "244556",
    specialties: [
      {
        name: "Coś tam i coś tam",
        grade: 3.56,
      },
      {
        name: "Jakaś bardzo długa nazwa, w koncu to PWR",
        grade: 3.96,
      },
    ],
  });

  return <ProfilePage user={userData} />;
};

export default ProfilePageContainer;
