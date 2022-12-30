// React
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
// API
import { getDestinations } from "api/universityApi";
// Components
import ListPage from "./ListPage";
import DestSpecialty from "models/DestSpecialty";

const ListPageContainer = () => {
  const navigate = useNavigate();
  const [destinations, setDestinations] = useState<DestSpecialty[]>([]);
  const [recommended, setRecommended] = useState<DestSpecialty[]>([]);
  const [recommendedByStudent, setRecommendedByStudent] = useState<DestSpecialty[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [totalAmount, setTotalAmount] = useState<number>(0);

  useEffect(() => {
    handlePageChange(0, 10);
  }, []);

  const handlePageChange = async (page: number, pageSize: number) => {
    setLoading(true);
    const currentPage = await getDestinations(page, pageSize);
    setDestinations(currentPage.destinations);
    setRecommended(currentPage.destinations.slice(0, 5));
    setRecommendedByStudent(currentPage.destinations.slice(5, 10));
    setTotalAmount(currentPage.totalRows);
    setLoading(false);
  };

  const handleOnClick = (id: number) => {
    navigate(`/list/${id}`);
  };

  return (
    <ListPage
      destinations={destinations}
      recommended={recommended}
      recommendedByStudent={recommendedByStudent}
      handlePageChange={handlePageChange}
      totalAmount={totalAmount}
      loading={loading}
      handleOnClick={handleOnClick}
    />
  );
};

export default ListPageContainer;
