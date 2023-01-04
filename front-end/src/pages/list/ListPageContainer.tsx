// React
import { useEffect, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
// API
import { getDestinations } from "api/universityApi";
// Components
import ListPage from "./ListPage";
import DestSpecialty from "models/DestSpecialty";
import { useAppDispatch, useAppSelector } from "storage/redux/hooks";
import { RootState } from "storage/redux/store";
import {
  fetchCountries,
  fetchDestinationsRecommended,
  fetchDestinationsRecommendedByStudents,
  fetchStudyAreas,
} from "storage/redux/universitySlice";
import SortingOptions from "api/DTOs/GET/SortingOptions";

const ListPageContainer = () => {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const { destinationsRecommended, destinationsRecommendedByStudents, countries, studyAreas } = useAppSelector(
    (state: RootState) => state.university
  );
  const { userLoggedIn } = useAppSelector((state: RootState) => state.login);
  const [destinations, setDestinations] = useState<DestSpecialty[]>([]);
  const [totalAmount, setTotalAmount] = useState<number>(0);
  const [loading, setLoading] = useState<boolean>(false);
  const sortingOptions = useRef(Object.values(SortingOptions));

  useEffect(() => {
    handlePageChange(0, 10);
  }, []);

  useEffect(() => {
    if (userLoggedIn) {
      if (destinationsRecommended === undefined) {
        dispatch(fetchDestinationsRecommended());
      }
      if (destinationsRecommendedByStudents === undefined) {
        dispatch(fetchDestinationsRecommendedByStudents());
      }
    }

    if (countries.length === 0) {
      dispatch(fetchCountries());
    }
    if (studyAreas.length === 0) {
      dispatch(fetchStudyAreas());
    }
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
      userLoggedIn={userLoggedIn}
      destinations={destinations}
      recommended={destinationsRecommended}
      recommendedByStudent={destinationsRecommendedByStudents}
      countries={countries}
      studyAreas={studyAreas}
      sortingOptions={sortingOptions.current}
      handlePageChange={handlePageChange}
      totalAmount={totalAmount}
      loading={loading}
      handleOnClick={handleOnClick}
    />
  );
};

export default ListPageContainer;
