// Ant Design
import { Input } from "antd";
// Styles
import "./ListPage.scss";
// Components
import DestSpecialty from "models/DestSpecialty";
import SelectFilter from "./SelectFilter";
import MainList from "./MainList";
import SideList from "./SideList";
import StudyArea from "models/StudyArea";

interface Props {
  userLoggedIn: boolean;
  destinations: DestSpecialty[];
  recommended: DestSpecialty[] | undefined;
  recommendedByStudent: DestSpecialty[] | undefined;
  countries: string[];
  studyAreas: StudyArea[];
  sortingOptions: string[];
  handlePageChange: (page: number, pageSize: number) => void;
  totalAmount: number;
  loading: boolean;
  handleOnClick: (id: number) => void;
}

const { Search } = Input;

const ListPage = ({
  userLoggedIn,
  destinations,
  recommended,
  recommendedByStudent,
  countries,
  studyAreas,
  sortingOptions,
  handlePageChange,
  totalAmount,
  loading,
  handleOnClick,
}: Props) => {
  const onSearch = (value: string) => console.log(value);

  return (
    <div className="list-page">
      <div className="block filter-section">
        <p className="header-font">FITLERS</p>
        <div className="filters">
          <SelectFilter
            label="Country"
            placeholder="Select country"
            options={countries.map(c => {
              return { value: crypto.randomUUID(), label: c };
            })}
          />
          <SelectFilter
            label="Subject area"
            placeholder="Select subject area"
            options={studyAreas.map(s => {
              return { value: s.id.toString(), label: s.areaName };
            })}
          />
          <SelectFilter
            label="Sort by"
            placeholder="Select sorting option"
            options={sortingOptions.map(s => {
              return { value: crypto.randomUUID(), label: s };
            })}
          />
          <div className="filter" style={{ marginLeft: "auto" }}>
            <p className="header-font">University name</p>
            <Search style={{ width: "350px" }} size="large" placeholder="input search text" onSearch={onSearch} />
          </div>
        </div>
      </div>
      <div className="lists">
        <div className="left-section">
          <div className="block list-space side-list">
            <p className="header-font">Recommended destinations</p>
            {userLoggedIn && recommended ? (
              <SideList destinations={recommended} loading={loading} handleOnClick={handleOnClick} />
            ) : (
              <p>Log in to see helpful recommendations!</p>
            )}
          </div>
          <div className="block list-space side-list">
            <p className="header-font">Students like you chose</p>
            {userLoggedIn && recommendedByStudent ? (
              <SideList destinations={recommendedByStudent} loading={loading} handleOnClick={handleOnClick} />
            ) : (
              <p>Log in to see helpful recommendations!</p>
            )}
          </div>
        </div>
        <div className="block list-space main-list">
          <MainList
            destinations={destinations}
            loading={loading}
            totalAmount={totalAmount}
            handleOnClick={handleOnClick}
            handlePageChange={handlePageChange}
          />
        </div>
      </div>
    </div>
  );
};

export default ListPage;
