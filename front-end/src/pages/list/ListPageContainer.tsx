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
  const [loading, setLoading] = useState<boolean>(false);
  const [totalAmount, setTotalAmount] = useState<number>(0);

  useEffect(() => {
    handlePageChange(0, 10);

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handlePageChange = async (page: number, pageSize: number) => {
    setLoading(true);
    const currentPage = await getDestinations(page, pageSize);
    setDestinations(currentPage.destinations);
    setTotalAmount(currentPage.totalRows);
    setLoading(false);
  };

  const handleOnClick = (id: number) => {
    navigate(`/list/${id}`);
  };

  return (
    <ListPage
      destinations={destinations}
      handlePageChange={handlePageChange}
      totalAmount={totalAmount}
      loading={loading}
      handleOnClick={handleOnClick}
    />
  );
};

export default ListPageContainer;
