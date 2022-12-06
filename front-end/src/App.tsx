import {
  Route,
  BrowserRouter as Router,
  Routes,
  Navigate,
} from "react-router-dom";
// Ant Design
import { Footer } from "antd/lib/layout/layout";
// Styles
import "./App.scss";
// Components
import Navbar from "components/nav/Navbar";
import ListPageContainer from "pages/list/ListPageContainer";
import ProfilePageContainer from "pages/profile/ProfilePageContainer";
import HomePage from "pages/home/HomePage";
import { useAppSelector } from "storage/redux/hooks";
import { RootState } from "storage/redux/store";
import Unauthorized from "components/Unauthorized";
import NotFound from "components/NotFound";
import LocationEventHandler from "components/LocationEventHandler";

function App() {
  const { userLoggedIn } = useAppSelector((state: RootState) => state.login);

  return (
    <div className="app">
      <Router>
        <Navbar />
        <div className="layout">
          <Routes>
            <Route path="/" element={<Navigate replace to="/home" />} />
            <Route path="/home" element={<HomePage />} />
            <Route path="/list" element={<ListPageContainer />} />
            <Route
              path="/profile"
              element={
                userLoggedIn ? <ProfilePageContainer /> : <Unauthorized />
              }
            />
            <Route path="*" element={<NotFound />} />
          </Routes>
          <Footer style={{ textAlign: "center" }}>
            Szampon Inc. Erasmus Sign-up
          </Footer>
        </div>
        <LocationEventHandler />
      </Router>
    </div>
  );
}

export default App;
