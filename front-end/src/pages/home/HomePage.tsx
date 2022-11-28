// Styles
import "./HomePage.scss";
// Assets
import globe from "assets/globe.svg";
import education from "assets/education.svg";
import trophy from "assets/trophy.svg";
// Components
import ImageTextCard from "components/ImageTextCard";

const HomePage = () => {
  return (
    <div className="home-page">
      <div className="jumbotron">
        <div className="text">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce tortor
          felis, tristique at elit ut, scelerisque dapibus justo. Fusce elit
          ante, feugiat eget suscipit quis, porta at dui. Aliquam erat.
        </div>
      </div>
      <div className="cards">
        <ImageTextCard>
          <div className="image-space">
            <img src={globe} alt="globe" />
          </div>
          <h3>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce
            tortor felis, tristique at elit ut, scelerisque dapibus justo. Fusce
            elit ante, feugiat eget suscipit quis, porta at dui. Aliquam erat.
          </h3>
        </ImageTextCard>
        <ImageTextCard>
          <div className="image-space">
            <img src={education} alt="globe" />
          </div>
          <h3>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce
            tortor felis, tristique at elit ut, scelerisque dapibus justo. Fusce
            elit ante, feugiat eget suscipit quis, porta at dui. Aliquam erat.
          </h3>
        </ImageTextCard>
        <ImageTextCard>
          <div className="image-space">
            <img src={trophy} alt="globe" />
          </div>
          <h3>
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce
            tortor felis, tristique at elit ut, scelerisque dapibus justo. Fusce
            elit ante, feugiat eget suscipit quis, porta at dui. Aliquam erat.
          </h3>
        </ImageTextCard>
      </div>
      <div className="button-space">
        <div className="login-button">Log in with USOS</div>
      </div>
    </div>
  );
};

export default HomePage;
