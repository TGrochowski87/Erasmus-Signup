import { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import NoteViewPage from "./NoteViewPage";

const NoteViewPageContainer = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [text, setText] = useState<string>("");

  const goBack = () => {
    navigate(-1);
  };

  const clearText = () => {
    setText("");
  };

  const saveNote = () => {};

  const deleteNote = () => {};

  return (
    <NoteViewPage
      text={text}
      setText={setText}
      goBack={goBack}
      clearText={clearText}
      saveNote={saveNote}
      deleteNote={deleteNote}
    />
  );
};

export default NoteViewPageContainer;
