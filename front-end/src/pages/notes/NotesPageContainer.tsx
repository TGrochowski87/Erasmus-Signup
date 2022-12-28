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
    if (notes === undefined) {
      dispatch(fetchNotesWithContent());
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const chooseNotesToRender = (): CommonNote[] => {
    if (notes === undefined) {
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
