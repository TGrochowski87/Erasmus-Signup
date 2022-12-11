import Opinion from "models/Opinion";
import { useState } from "react";
import DestinationDetailsPage from "./DestinationDetailsPage";

const DestinationDetailsPageContainer = () => {
  const [opinionInput, setOpinionInput] = useState<string>("");
  const [ratingInput, setRatingInput] = useState<number>(0);

  const [opinions, setOpinions] = useState<Opinion[]>([
    {
      id: 0,
      name: "Anonymous",
      text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
      rating: 31,
    },
    {
      id: 1,
      name: "Anonymous",
      text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
      rating: 31,
    },
    {
      id: 2,
      name: "Anonymous",
      text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
      rating: 31,
    },
    {
      id: 3,
      name: "Anonymous",
      text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
      rating: 31,
    },
    {
      id: 4,
      name: "Anonymous",
      text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
      rating: 31,
    },
  ]);

  return (
    <DestinationDetailsPage
      opinionInput={opinionInput}
      setOpinionInput={setOpinionInput}
      ratingInput={ratingInput}
      setRatingInput={setRatingInput}
      opinions={opinions}
    />
  );
};

export default DestinationDetailsPageContainer;
