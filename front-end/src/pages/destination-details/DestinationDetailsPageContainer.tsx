import Opinion from "models/Opinion";
import { useEffect, useState } from "react";
import DestinationDetailsPage from "./DestinationDetailsPage";

const DestinationDetailsPageContainer = () => {
  const [opinionInput, setOpinionInput] = useState<string>("");
  const [ratingInput, setRatingInput] = useState<number>(0);

  const [loadingOpinions, setOpinionLoading] = useState<boolean>(false);
  const [opinions, setOpinions] = useState<Opinion[]>([
    {
      id: 0,
      name: "Anonymous",
      text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
      rating: 31,
    },
  ]);

  // TODO: Remove mock
  const appendData = () => {
    if (loadingOpinions) {
      return;
    }
    setOpinionLoading(true);

    console.log("test2");
    const newOpinions = [];
    for (let i = 1; i <= 10; i++) {
      newOpinions.push({
        id: Math.max(...opinions.map((o) => o.id)) + i,
        name: "Anonymous",
        text: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
        rating: 31,
      });
    }
    console.log(newOpinions);
    setOpinions(opinions.concat(newOpinions));
    setOpinionLoading(false);
  };

  const onScroll = (e: MouseEvent) => {
    console.log(e.clientY);
    // if (e.clientY - e.currentTarget.scrollTop === 600) {
    //   appendData();
    // }
  };

  useEffect(() => {
    appendData();
  }, []);

  return (
    <DestinationDetailsPage
      opinionInput={opinionInput}
      setOpinionInput={setOpinionInput}
      ratingInput={ratingInput}
      setRatingInput={setRatingInput}
      opinions={opinions}
      setOpinions={setOpinions}
      loadingOpinions={loadingOpinions}
      setOpinionLoading={setOpinionLoading}
      onScroll={onScroll}
      appendData={appendData}
    />
  );
};

export default DestinationDetailsPageContainer;
