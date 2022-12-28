// React
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
// Redux
import { useAppDispatch, useAppSelector } from "storage/redux/hooks";
import { fetchNotesWithContent } from "storage/redux/noteSlice";
import { RootState } from "storage/redux/store";
// Components
import CommonNote from "models/notes/CommonNote";
import NotesPage from "./NotesPage";

const NotesPageContainer = () => {
  const navigate = useNavigate();
  const { loading, notes } = useAppSelector((state: RootState) => state.note);
  const dispatch = useAppDispatch();
  const [activeTab, setActiveTab] = useState<"COMMON" | "SPECIALTIES" | "PLANS">("COMMON");

  useEffect(() => {
    const { common, speciality, plan } = notes;
    if (common.length === 0 && speciality.length === 0 && plan.length === 0) {
      dispatch(fetchNotesWithContent());
    }
  }, []);

  const chooseNotesToRender = (): CommonNote[] => {
    const { common, speciality, plan } = notes;
    if (common.length === 0 && speciality.length === 0 && plan.length === 0) {
      return [];
    }

    switch (activeTab) {
      case "COMMON":
        return notes.common;
      case "SPECIALTIES":
        return notes.speciality;
      case "PLANS":
        return notes.plan;
    }
  };

  return (
    <NotesPage
      notes={chooseNotesToRender()}
      loading={loading}
      activeTab={activeTab}
      setActiveTab={setActiveTab}
      navigate={navigate}
    />
  );
};

export default NotesPageContainer;
