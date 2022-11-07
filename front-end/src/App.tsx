import ExamplePage from "pages/example/ExamplePage";
import ListPageContainer from "pages/list/ListPageContainer";
import { Route, BrowserRouter as Router, Routes } from "react-router-dom";
import "./App.scss";
import ExampleHomePage from "./pages/home/ExampleHomePage";

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path="/" element={<ExampleHomePage />} />
          <Route path="/list" element={<ListPageContainer />} />
          <Route path="/example" element={<ExamplePage />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
