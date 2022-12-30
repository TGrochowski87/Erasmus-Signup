// Ant Design
import { Input } from "antd";
// Styles
import "./ListPage.scss";
// Components
import DestSpecialty from "models/DestSpecialty";
import SelectFilter from "./SelectFilter";
import MainList from "./MainList";
import SideList from "./SideList";

interface Props {
  destinations: DestSpecialty[];
  recommended: DestSpecialty[];
  recommendedByStudent: DestSpecialty[];
  handlePageChange: (page: number, pageSize: number) => void;
  totalAmount: number;
  loading: boolean;
  handleOnClick: (id: number) => void;
}

const countries = [
  {
    value: "poland",
    label: "Poland",
  },
  {
    value: "austria",
    label: "Austria",
  },
  {
    value: "germany",
    label: "Germany",
  },
];

const { Search } = Input;

const ListPage = ({
  destinations,
  recommended,
  recommendedByStudent,
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
          <SelectFilter label="Country" placeholder="Select country" options={countries} />
          <SelectFilter label="Subject area" placeholder="Select subject area" options={countries} />
          <SelectFilter label="Sort by" placeholder="Select sorting option" options={countries} />
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
            <SideList destinations={recommended} loading={loading} handleOnClick={handleOnClick} />
          </div>
          <div className="block list-space side-list">
            <p className="header-font">Students like you chose</p>
            <SideList destinations={recommendedByStudent} loading={loading} handleOnClick={handleOnClick} />
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
