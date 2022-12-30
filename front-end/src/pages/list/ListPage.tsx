// Ant Design
import { LineChartOutlined, MessageOutlined, TeamOutlined } from "@ant-design/icons";
import { Button, List, Rate } from "antd";
// Components
import InlineItems from "components/InlineItems";
import DestSpecialty from "models/DestSpecialty";
// Styles
import "./ListPage.scss";
import openInNewTab from "utilities/openInNewTab";
import FavoriteStatusIndicator from "components/FavoriteStatusIndicator";

interface Props {
  destinations: DestSpecialty[];
  handlePageChange: (page: number, pageSize: number) => void;
  totalAmount: number;
  loading: boolean;
  handleOnClick: (id: number) => void;
}

const ListPage = ({ destinations, handlePageChange, totalAmount, loading, handleOnClick }: Props) => {
  return (
    <div>
      <div
        style={{
          marginTop: 64,
          width: "70%",
          marginLeft: "auto",
        }}>
        <div className="site-layout-background" style={{ padding: 24, minHeight: 380 }}>
          <List
            loading={loading}
            bordered
            itemLayout="vertical"
            dataSource={destinations}
            footer={
              <div>
                <b>Erasmus Sign up</b> destinations
              </div>
            }
            pagination={{
              position: "both",
              total: totalAmount,
              pageSize: 10,
              onChange: (page, pageSize) => {
                handlePageChange(page, pageSize);
              },
            }}
            renderItem={item => (
              <List.Item
                key={item.destinationSpecialityId}
                className="university-list-item"
                style={{
                  borderBottomColor: "rgb(184, 184, 184)",
                  paddingLeft: "0.5rem",
                }}
                onClick={() => {
                  handleOnClick(item.destinationSpecialityId);
                }}>
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
                          }}>
                          {item.universityName}
                        </h2>
                        <h3
                          style={{
                            display: "inline",
                            color: "grey",
                          }}>
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
                        <FavoriteStatusIndicator active={item.isObserved} />
                      </div>

                      <p style={{ marginTop: "0.5rem" }}>
                        {item.subjectAreaName} | {item.subjectAreaId}
                      </p>
                    </div>

                    <div className="text-icons">
                      <InlineItems>
                        <TeamOutlined />
                        {item.places}
                      </InlineItems>

                      <InlineItems>
                        <LineChartOutlined />
                        {item.average}
                      </InlineItems>

                      <InlineItems>
                        <MessageOutlined />
                        {item.opinions}
                      </InlineItems>
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
                        if (item.link !== null) {
                          openInNewTab(item.link);
                        } else {
                          alert("No website for this university.");
                        }
                      }}>
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
