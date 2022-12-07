// React
import { useParams } from "react-router-dom";
// Ant Design
import { Avatar, Button, Divider, List, Rate, Skeleton, Table } from "antd";
import Column from "antd/lib/table/Column";
import {
  CompassOutlined,
  ContainerOutlined,
  HeartOutlined,
  UserOutlined,
} from "@ant-design/icons";
import VirtualList from "rc-virtual-list";
// Assets
import call from "assets/call.svg";
import email from "assets/email.svg";
import location from "assets/location.svg";
import Tag from "assets/tag.svg";
// Styles
import "./DestinationDetailsPage.scss";
// Components
import InlineItems from "components/InlineItems";
import TextArea from "antd/lib/input/TextArea";
import Opinion from "models/Opinion";
import { UIEventHandler } from "react";
import InfiniteScroll from "react-infinite-scroll-component";

interface Props {
  opinionInput: string;
  setOpinionInput: React.Dispatch<React.SetStateAction<string>>;
  ratingInput: number;
  setRatingInput: React.Dispatch<React.SetStateAction<number>>;
  opinions: Opinion[];
  setOpinions: React.Dispatch<React.SetStateAction<Opinion[]>>;
  loadingOpinions: boolean;
  setOpinionLoading: React.Dispatch<React.SetStateAction<boolean>>;
  onScroll: (e: MouseEvent) => any; //UIEventHandler<HTMLElement>;
  appendData: () => any;
}

// TODO: Remove from here
interface SubjectMicro {
  subjectArea: string;
  language: string;
  vacancies: number;
}

// TODO: Remove
const data: SubjectMicro[] = [
  // API powinno zwrócić tak, że te bez miejsc są na dole
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
  {
    subjectArea: "History and archaeology | 222",
    language: "French",
    vacancies: 2,
  },
];

const DestinationDetailsPage = ({
  opinionInput,
  setOpinionInput,
  ratingInput,
  setRatingInput,
  opinions,
  setOpinions,
  loadingOpinions,
  setOpinionLoading,
  onScroll,
  appendData,
}: Props) => {
  //const { code, id } = useParams(); - will be used for requesting data

  return (
    <div className="details-page">
      <div className="all-data">
        <div className="block university-data">
          <InlineItems style={{ gap: "1rem" }}>
            <img
              style={{ width: "70px" }}
              src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Flag_of_France_%281794%E2%80%931815%2C_1830%E2%80%931974%2C_2020%E2%80%93present%29.svg/240px-Flag_of_France_%281794%E2%80%931815%2C_1830%E2%80%931974%2C_2020%E2%80%93present%29.svg.png"
              alt="country flag"
            />
            <h1>Université Lumière (Lyon II)</h1>
          </InlineItems>

          <div className="details">
            <InlineItems>
              <img src={Tag} alt="globe" />
              <h2>ErAsmUS-CODE</h2>
            </InlineItems>

            <InlineItems>
              <img src={location} alt="globe" />
              <h2>France, Lyon</h2>
            </InlineItems>

            <InlineItems>
              <img src={email} alt="globe" />
              <h2>lyon.france@mail.com</h2>
            </InlineItems>

            <InlineItems>
              <img src={call} alt="globe" />
              <h2>123123123</h2>
            </InlineItems>
          </div>
        </div>

        <div className="block specialty-list">
          <h2 className="header">
            AVAILABLE DESTINATIONS{" "}
            <CompassOutlined style={{ marginLeft: "0.5rem" }} />
          </h2>
          <Table
            dataSource={data}
            pagination={false}
            loading={false} // will be used with requesting data
            size="small"
            scroll={{ y: 240 }}
            bordered
          >
            <Column
              title="Subject Area"
              dataIndex="subjectArea"
              key="subjectArea"
            />
            <Column
              title="Language"
              dataIndex="language"
              key="language"
              width={150}
            />
            <Column
              title="Vacancies"
              dataIndex="vacancies"
              key="vacancies"
              width={100}
            />
          </Table>
        </div>
      </div>

      <div className="block specialty-data" style={{ position: "relative" }}>
        <h1>History and archaeology | 222</h1>
        <h2>Vacancy: 4</h2>
        <h2>Last year's required grade: 3,94</h2>
        <h2>Rating: 4,3</h2>
        <h2>Currently interested students: 7</h2>
        <div style={{ position: "absolute", top: "20px", right: "50px" }}>
          <ContainerOutlined
            style={{ marginRight: "2rem", fontSize: "1.5rem" }}
          />
          <HeartOutlined
            style={{
              fontSize: "1.5rem",
            }}
          />
        </div>
      </div>

      <div className="block opinions">
        <Divider className="header">OPINIONS</Divider>
        <div className="input-space">
          <Rate
            style={{ marginBottom: "5px" }}
            allowHalf
            value={ratingInput}
            onChange={setRatingInput}
          />
          <TextArea
            placeholder="Share your opinion here..."
            autoSize={{ minRows: 5 }}
            maxLength={500}
            allowClear
            showCount
            value={opinionInput}
            onChange={(event) => {
              setOpinionInput(event.target.value);
            }}
          />
          <Button
            style={{ marginTop: "10px", padding: "0 2rem" }}
            size="large"
            type="primary"
          >
            Share opinion
          </Button>
        </div>
        <Divider></Divider>
        <div className="list">
          <div
            id="scrollableDiv"
            style={{
              overflow: "auto",
              padding: "0 16px",
            }}
          >
            <InfiniteScroll
              dataLength={opinions.length}
              next={appendData}
              onScroll={onScroll}
              hasMore={opinions.length < 50}
              loader={<Skeleton avatar paragraph={{ rows: 1 }} active />}
              endMessage={<Divider plain>We are all out of opinions.</Divider>}
              scrollableTarget="scrollableDiv"
            >
              <List
                dataSource={opinions}
                renderItem={(item) => (
                  <List.Item style={{ position: "relative" }} key={item.id}>
                    <List.Item.Meta
                      avatar={<Avatar size={64} icon={<UserOutlined />} />}
                      title={<h2>{item.name}</h2>}
                    />
                    <Rate
                      className="rate"
                      allowHalf
                      value={item.rating}
                      disabled
                    />
                    {item.text}
                  </List.Item>
                )}
              />
            </InfiniteScroll>
          </div>
          {/* <List>
            <VirtualList
              data={opinions}
              itemHeight={47}
              itemKey="id"
              onScroll={onScroll}
              height={600}
            >
              {(item: Opinion) => (
                <List.Item
                  key={item.id}
                  extra={<Rate allowHalf value={item.rating} disabled />}
                >
                  <List.Item.Meta
                    avatar={<Avatar size={64} icon={<UserOutlined />} />}
                    title={item.name}
                  />
                  {item.text}
                </List.Item>
              )}
            </VirtualList>
          </List> */}
        </div>
      </div>
    </div>
  );
};

export default DestinationDetailsPage;
