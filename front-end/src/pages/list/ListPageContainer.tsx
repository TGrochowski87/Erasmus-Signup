// Redux
import { useAppDispatch, useAppSelector } from "storage/redux/hooks";
import { RootState } from "storage/redux/store";
import { useEffect } from "react";
import { fetchUniversities } from "storage/redux/universitySlice";
// Components
import ListPage from "./ListPage";
import { useNavigate } from "react-router-dom";

const ListPageContainer = () => {
  const navigate = useNavigate();
  const universities = useAppSelector((state: RootState) => state.university.value);
  const dispatch = useAppDispatch();

  useEffect(() => {
    if (universities.length === 0) {
      dispatch(fetchUniversities());
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleOnClick = (code: string, id: number) => {
    navigate(`/list/${code}/${id}`);
  };

  return <ListPage universities={universities} handleOnClick={handleOnClick} />;
};

export default ListPageContainer;
