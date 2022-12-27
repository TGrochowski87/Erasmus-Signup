import { EmptyUser } from "models/User";
import { useAppDispatch, useAppSelector } from "storage/redux/hooks";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ProfilePage from "./ProfilePage";
import { fetchUserCurrent } from "storage/redux/userCurrentSlice";
import { RootState } from "storage/redux/store";

const ProfilePageContainer = () => {
  const navigate = useNavigate();

  const currentUser = useAppSelector((state: RootState) => state.userCurrent.value);

  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(fetchUserCurrent())
  }, [dispatch]);

  const navigateToNotesPage = () => {
    navigate("/notes");
  };

  const navigateToPlansPage = () => {
    navigate("/plans");
  };

  const navigateToSubjectsPage = () => {
    navigate("/subjects");
  };

  return <ProfilePage
    user={currentUser ? currentUser : EmptyUser}
    navigateToNotesPage={navigateToNotesPage}
    navigateToPlansPage={navigateToPlansPage}
    navigateToSubjectsPage={navigateToSubjectsPage}
  />;
};

export default ProfilePageContainer;
