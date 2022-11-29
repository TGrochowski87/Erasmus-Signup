// Styles
import { Spin } from "antd";
import "./Components.scss";

const FullViewLoading = () => {
  return (
    <div className="full-page-loading-container">
      <Spin size="large" />
    </div>
  );
};

export default FullViewLoading;
