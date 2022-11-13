import { Layout } from "antd";
import { Footer } from "antd/lib/layout/layout";
import Navbar from "components/nav/Navbar";
import ListPageContainer from "pages/list/ListPageContainer";
import ProfilePageContainer from "pages/profile/ProfilePageContainer";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import "./App.scss";
import ExampleHomePage from "./pages/home/ExampleHomePage";

function App() {
  return (
    <div className="App">
      <Router>
        <Layout>
          <Navbar />
          <Routes>
            <Route path="/" element={<ExampleHomePage />} />
            <Route path="/list" element={<ListPageContainer />} />
            <Route path="/profile" element={<ProfilePageContainer />} />
          </Routes>
          <Footer style={{ textAlign: "center" }}>
            Ant Design ©2018 Created by Ant UED
          </Footer>
        </Layout>
      </Router>
    </div>
  );
}

export default App;
