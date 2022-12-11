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
import InfiniteScroll from "react-infinite-scroll-component";
import BlockLabeledTextField from "components/BlockLabeledTextField";

interface Props {
  opinionInput: string;
  setOpinionInput: React.Dispatch<React.SetStateAction<string>>;
  ratingInput: number;
  setRatingInput: React.Dispatch<React.SetStateAction<number>>;
  opinions: Opinion[];
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
}: Props) => {
  //const { code, id } = useParams(); - will be used for requesting data

  return (
    <div className="details-page">
      <div className="all-data">
        <div className="block university-data">
          <div className="university-name">
            <InlineItems>
              <img
                src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/bc/Flag_of_France_%281794%E2%80%931815%2C_1830%E2%80%931974%2C_2020%E2%80%93present%29.svg/240px-Flag_of_France_%281794%E2%80%931815%2C_1830%E2%80%931974%2C_2020%E2%80%93present%29.svg.png"
                alt="country flag"
              />
              <h1>Université Lumière (Lyon II)</h1>
            </InlineItems>
          </div>

          <div className="details">
            <BlockLabeledTextField label="Erasmus code" text="ErAsmUS-CODE" />
            <BlockLabeledTextField label="Location" text="France, Lyon" />
            <BlockLabeledTextField
              label="Contact"
              text="lyon.france@mail.com"
            />
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
        <div className="grid">
          <BlockLabeledTextField label="Vacancy" text="4" />
          <BlockLabeledTextField label="Rating" text="4,3" />
          <BlockLabeledTextField label="Last year's min. grade" text="3,94" />
          <BlockLabeledTextField label="Currently interested" text="7" />
        </div>
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
          </div>
        </div>
      </div>
    </div>
  );
};

export default DestinationDetailsPage;
