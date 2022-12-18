// React
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
// Components
import Opinion from "models/Opinion";
import DestinationDetailsPage from "./DestinationDetailsPage";
import GetDestinationDetailsResponse from "api/DTOs/GET/GetDestinationDetailsResponse";
import { getDestinationDetails } from "api/universityApi";

const DestinationDetailsPageContainer = () => {
  const { id } = useParams();
  const [detailsData, setDetailsData] = useState<GetDestinationDetailsResponse | undefined>(undefined);
  const [selectedDestId, setSelectedDestId] = useState<number>(0);
  const [loading, setLoading] = useState<{ details: boolean; opinions: boolean }>({
    details: false,
    opinions: false,
  });
  const [opinionInput, setOpinionInput] = useState<string>("");
  const [ratingInput, setRatingInput] = useState<number>(0);

  useEffect(() => {
    if (detailsData === undefined) {
      fetchDetailsData();
    }
  }, []);

  const fetchDetailsData = async () => {
    setLoading({ ...loading, details: true });
    const details = await getDestinationDetails(parseInt(id!));
    setDetailsData(details);
    setSelectedDestId(details.selectedDestId);
    setLoading({ ...loading, details: false });
  };

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
      detailsData={detailsData}
      selectedDestId={selectedDestId}
      setSelectedDestId={setSelectedDestId}
      opinionInput={opinionInput}
      setOpinionInput={setOpinionInput}
      ratingInput={ratingInput}
      setRatingInput={setRatingInput}
      opinions={opinions}
      loading={loading}
    />
  );
};

export default DestinationDetailsPageContainer;
