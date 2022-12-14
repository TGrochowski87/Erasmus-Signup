// Styles
import { List, Typography } from "antd";
import "./NotesPage.scss";

interface Props {
  notes: string[];
  activeTab: "COMMON" | "SPECIALTIES" | "PLANS";
  setActiveTab: React.Dispatch<React.SetStateAction<"COMMON" | "SPECIALTIES" | "PLANS">>;
}

const NotesPage = ({ notes, activeTab, setActiveTab }: Props) => {
  return (
    <div className="notes-page">
      <div className="tabs">
        <div className={`tab ${activeTab === "COMMON" ? "active" : ""}`} onClick={() => setActiveTab("COMMON")}>
          <p>COMMON</p>
        </div>
        <div
          className={`tab ${activeTab === "SPECIALTIES" ? "active" : ""}`}
          onClick={() => setActiveTab("SPECIALTIES")}>
          <p>SPECIALTIES</p>
        </div>
        <div className={`tab ${activeTab === "PLANS" ? "active" : ""}`} onClick={() => setActiveTab("PLANS")}>
          <p>PLANS</p>
        </div>
      </div>

      <div className="block notes-space">
        <List
          dataSource={notes}
          renderItem={item => (
            <List.Item className="list-item">
              <Typography.Text strong ellipsis={true}>
                {item}
              </Typography.Text>
            </List.Item>
          )}
        />
      </div>
    </div>
  );
};

export default NotesPage;
