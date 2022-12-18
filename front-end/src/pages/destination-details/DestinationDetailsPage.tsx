// Ant Design
import { Avatar, Button, Divider, List, Rate, Table } from "antd";
import Column from "antd/lib/table/Column";
import { CompassOutlined, UserOutlined } from "@ant-design/icons";
// Styles
import "./DestinationDetailsPage.scss";
// Components
import InlineItems from "components/InlineItems";
import TextArea from "antd/lib/input/TextArea";
import Opinion from "models/Opinion";
import BlockLabeledTextField from "components/BlockLabeledTextField";
import GetDestinationDetailsResponse from "api/DTOs/GET/GetDestinationDetailsResponse";
import FullViewLoading from "components/FullViewLoading";
import FavoriteStatusIndicator from "components/FavoriteStatusIndicator";
import NoteStatusIndicator from "components/NoteStatusIndicator";

interface Props {
  detailsData: GetDestinationDetailsResponse | undefined;
  selectedDestId: number;
  setSelectedDestId: React.Dispatch<React.SetStateAction<number>>;
  opinionInput: string;
  setOpinionInput: React.Dispatch<React.SetStateAction<string>>;
  ratingInput: number;
  setRatingInput: React.Dispatch<React.SetStateAction<number>>;
  opinions: Opinion[];
  loading: { details: boolean; opinions: boolean };
}

// TODO: Remove from here
interface DestMicro {
  id: number;
  subjectArea: string;
  language: string;
  vacancies: number;
}

const DestinationDetailsPage = ({
  detailsData,
  selectedDestId,
  setSelectedDestId,
  opinionInput,
  setOpinionInput,
  ratingInput,
  setRatingInput,
  opinions,
  loading,
}: Props) => {
  const specialty = detailsData?.destinations.find(d => d.id === selectedDestId)!;

  return detailsData ? (
    <div className="details-page">
      <div className="all-data">
        <div className="block university-data">
          <div className="university-name">
            <InlineItems>
              <img src={detailsData.flagUrl} alt="country flag" />
              <h1>{detailsData.universityName}</h1>
            </InlineItems>
          </div>

          <div className="details">
            <BlockLabeledTextField label="Erasmus code" text={detailsData.erasmusCode} />
            <BlockLabeledTextField label="Location" text={`${detailsData.country}, ${detailsData.city}`} />
            <BlockLabeledTextField label="Contact" text={detailsData.email} />
          </div>
        </div>

        <div className="block specialty-list">
          <h2 className="header">
            AVAILABLE DESTINATIONS <CompassOutlined style={{ marginLeft: "0.5rem" }} />
          </h2>
          <Table
            dataSource={detailsData.destinations.map<DestMicro>(dest => {
              return {
                id: dest.id,
                subjectArea: `${dest.subjectAreaName} | ${dest.subjectAreaId}`,
                language: dest.subjectLanguageName,
                vacancies: dest.places,
              };
            })}
            onRow={record => {
              return {
                onClick: event => {
                  setSelectedDestId(record.id);
                },
              };
            }}
            pagination={false}
            loading={loading.details} // will be used with requesting data
            size="small"
            scroll={{ y: 240 }}
            bordered>
            <Column title="Subject Area" dataIndex="subjectArea" key="subjectArea" />
            <Column title="Language" dataIndex="language" key="language" width={150} />
            <Column title="Vacancies" dataIndex="vacancies" key="vacancies" width={100} />
          </Table>
        </div>
      </div>

      <div className="block specialty-data" style={{ position: "relative" }}>
        <h1>{`${specialty?.subjectAreaName} | ${specialty.subjectAreaId}`}</h1>

        <div className="grid">
          <BlockLabeledTextField label="Vacancy" text={specialty.places} />
          <BlockLabeledTextField label="Rating" text={specialty.rating} />
          <BlockLabeledTextField label="Previous min. grade" text={specialty.average} />
          <BlockLabeledTextField label="Currently interested" text={specialty.interestedStudents} />
        </div>

        <div className="icon-space">
          <NoteStatusIndicator active={specialty.isNote} />
          <FavoriteStatusIndicator active={specialty.isObserved} />
        </div>
      </div>

      <div className="block opinions">
        <Divider className="header">OPINIONS</Divider>
        <div className="input-space">
          <Rate style={{ marginBottom: "5px" }} allowHalf value={ratingInput} onChange={setRatingInput} />
          <TextArea
            placeholder="Share your opinion here..."
            autoSize={{ minRows: 5 }}
            maxLength={500}
            allowClear
            showCount
            value={opinionInput}
            onChange={event => {
              setOpinionInput(event.target.value);
            }}
          />
          <Button style={{ marginTop: "10px", padding: "0 2rem" }} size="large" type="primary">
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
            }}>
            <List
              dataSource={opinions}
              renderItem={item => (
                <List.Item style={{ position: "relative" }} key={item.id}>
                  <List.Item.Meta avatar={<Avatar size={64} icon={<UserOutlined />} />} title={item.name} />
                  <Rate className="rate" allowHalf value={item.rating} disabled />
                  {item.text}
                </List.Item>
              )}
            />
          </div>
        </div>
      </div>
    </div>
  ) : (
    <FullViewLoading />
  );
};

export default DestinationDetailsPage;
