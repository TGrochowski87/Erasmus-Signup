// Ant Design
import { Button, Menu } from "antd";
import { Header } from "antd/lib/layout/layout";
// Styles
import "./Navbar.scss";
// Assets
import pwrlogo from "assets/pwr.webp";
import eulogo from "assets/erasmus.webp";
import { useNavigate } from "react-router-dom";

const Navbar = () => {
  const navigate = useNavigate();

  return (
    <div className="nav">
      <img src={pwrlogo} alt="pwr logo" />
      <img src={eulogo} alt="eu logo" />
      <div style={{ marginLeft: "auto" }}>
        <Button
          onClick={() => {
            navigate("/");
          }}
        >
          Home
        </Button>
        <Button
          onClick={() => {
            navigate("/list");
          }}
        >
          Destinations
        </Button>
        <Button
          onClick={() => {
            navigate("/profile");
          }}
        >
          Profile
        </Button>
      </div>
    </div>
  );
};

export default Navbar;
