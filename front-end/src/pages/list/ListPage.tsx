// Ant Design
import {
  HeartFilled,
  HeartOutlined,
  LineChartOutlined,
  MessageOutlined,
  TeamOutlined,
} from "@ant-design/icons";
import { Button, List, Rate } from "antd";
// Components
import University from "models/University";
import IconText from "components/IconText";
// Styles
import "./ListPage.scss";
import openInNewTab from "utilities/openInNewTab";

interface Props {
  universities: University[];
}

const ListPage = ({ universities }: Props) => {
  return (
    <div>
      <div
        style={{
          marginTop: 64,
          width: "70%",
          marginLeft: "auto",
        }}
      >
        <div
          className="site-layout-background"
          style={{ padding: 24, minHeight: 380 }}
        >
          <List
            loading={universities.length === 0}
            bordered
            itemLayout="vertical"
            dataSource={universities}
            footer={
              <div>
                <b>Erasmus Sign up</b> destinations
              </div>
            }
            renderItem={(item) => (
              <List.Item
                key={item.id}
                className="university-list-item"
                style={{
                  borderBottomColor: "rgb(184, 184, 184)",
                  paddingLeft: "0.5rem",
                }}
              >
                <div className="university-list-item-content">
                  <div className="country-flag-space">
                    <img src={item.flagUrl} alt="country flag" />
                  </div>
                  <div className="university-data-space">
                    <div className="university-information">
                      <div className="top-row">
                        <h2
                          style={{
                            display: "inline",
                            marginRight: "10px",
                          }}
                        >
                          {item.universityName}
                        </h2>
                        <h3
                          style={{
                            display: "inline",
                            color: "grey",
                          }}
                        >
                          {item.country}
                        </h3>
                        <Rate
                          disabled
                          allowHalf
                          defaultValue={item.rating}
                          style={{
                            margin: "0 auto",
                            position: "relative",
                            top: "-10px",
                          }}
                        />
                        {item.isObserved ? (
                          <HeartFilled
                            style={{
                              fontSize: "1.5rem",
                              color: "red",
                            }}
                          />
                        ) : (
                          <HeartOutlined
                            style={{
                              fontSize: "1.5rem",
                            }}
                          />
                        )}
                      </div>

                      <p style={{ marginTop: "0.5rem" }}>
                        {item.subjectAreaName} | {item.subjectAreaId}
                      </p>
                    </div>

                    <div className="text-icons">
                      <IconText
                        icon={TeamOutlined}
                        text={item.availablePlaces}
                        key="list-vertical-star-o"
                      />
                      <IconText
                        icon={LineChartOutlined}
                        text={item.lastYearGradeAvg}
                        key="list-vertical-like-o"
                      />
                      <IconText
                        icon={MessageOutlined}
                        text={item.opinionsAmount}
                        key="list-vertical-message"
                      />
                    </div>
                  </div>

                  <div className="buttons">
                    <Button size="large" type="primary">
                      Show on map
                    </Button>
                    <Button
                      size="large"
                      type="primary"
                      onClick={() => {
                        if (item.website !== null) {
                          openInNewTab(item.website);
                        } else {
                          alert("No website for this university.");
                        }
                      }}
                    >
                      Visit website
                    </Button>
                  </div>
                </div>
              </List.Item>
            )}
          />
        </div>
      </div>
    </div>
  );
};

export default ListPage;
