import Note from "models/Note";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import NotesPage from "./NotesPage";

const data: Note[] = [
  {
    id: 1,
    content:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
  {
    id: 1,
    content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
  },
];

const NotesPageContainer = () => {
  const navigate = useNavigate();
  const [notes, setNotes] = useState<Note[]>([]);
  const [activeTab, setActiveTab] = useState<"COMMON" | "SPECIALTIES" | "PLANS">("COMMON");

  useEffect(() => {
    setNotes(data);
  }, []);

  return <NotesPage notes={notes} activeTab={activeTab} setActiveTab={setActiveTab} navigate={navigate} />;
};

export default NotesPageContainer;
