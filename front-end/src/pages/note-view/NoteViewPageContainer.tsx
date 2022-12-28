import PostCommonNote from "api/DTOs/POST/PostCommonNote";
import { postCommonNote } from "api/noteApi";
import CommonNote from "models/notes/CommonNote";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "storage/redux/hooks";
import { addCommonNoteLocally } from "storage/redux/noteSlice";
import { RootState } from "storage/redux/store";
import NoteViewPage from "./NoteViewPage";

const NoteViewPageContainer = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { common, speciality, plan } = useAppSelector((state: RootState) => state.note.notes);
  const dispatch = useAppDispatch();
  const [title, setTitle] = useState<string>("");
  const [text, setText] = useState<string>("");

  useEffect(() => {
    if (id !== undefined) {
      let allNotes: CommonNote[] = [...common, ...speciality, ...plan];
      console.log(allNotes);
    }
  }, []);

  const goBack = () => {
    navigate(-1);
  };

  const clearText = () => {
    setText("");
  };

  const saveNote = async () => {
    const body: PostCommonNote = {
      title: title,
      content: text,
    };

    await postCommonNote(body);
    dispatch(addCommonNoteLocally(body));
    goBack();
  };

  const deleteNote = () => {};

  return (
    <NoteViewPage
      text={text}
      setText={setText}
      title={title}
      setTitle={setTitle}
      goBack={goBack}
      clearText={clearText}
      saveNote={saveNote}
      deleteNote={deleteNote}
    />
  );
};

export default NoteViewPageContainer;
