// Ant Design
import {
  HeartFilled,
  HeartOutlined,
  LineChartOutlined,
  MessageOutlined,
  TeamOutlined,
} from "@ant-design/icons";
import { Button, Layout, List, Menu, Rate } from "antd";
import { Content, Footer, Header } from "antd/lib/layout/layout";
// Components
import University from "models/University";
import IconText from "components/IconText";
// Styles
import "./ListPage.scss";
import openInNewTab from "utilities/openInNewTab";
// tools
import { getCookie, setCookie } from 'typescript-cookie'

interface Props {
  universities: University[];
}

const ListPage = ({ universities }: Props) => {
  return (
    <Layout>
      <Header style={{ position: "fixed", zIndex: 1, width: "100%" }}>
        <div className="logo" />
        <Menu
          theme="dark"
          mode="horizontal"
          defaultSelectedKeys={["2"]}
          items={new Array(3).fill(null).map((_, index) => ({
            key: String(index + 1),
            label: `nav ${index + 1}`,
          }))}
        />

        { getCookie("oauth_token") != null && getCookie("oauth_verifier") != null ?
          <div>You're logged in!</div>
        :
          <button onClick={() =>{
            window.location.assign("/login?loginCallbackPath="+encodeURIComponent(window.location.href.slice(window.location.origin.length+1)));
          }}>
            Log me in
          </button>
        }
      </Header>
      <Layout>
        <Content
          className="site-layout"
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
        </Content>
      </Layout>
      <Footer style={{ textAlign: "center" }}>
        Ant Design ©2018 Created by Ant UED
      </Footer>
    </Layout>
  );
};

export default ListPage;
