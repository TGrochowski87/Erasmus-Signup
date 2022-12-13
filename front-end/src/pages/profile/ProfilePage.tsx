import { Image, Table } from "antd";
import UnderlinedLabeledTextField from "components/UnderlinedLabeledTextField";
import User from "models/User";
// Styles
import "./ProfilePage.scss";

interface Props {
  user: User;
}

const ProfilePage = ({ user }: Props) => {
  return (
    <div className="user-page">
      <div className="user-section">
        <div className="block personal-data">
          <div className="image-space">
            <Image
              width={160}
              height={180}
              src="https://image.shutterstock.com/image-photo/passport-photo-portrait-middle-aged-260nw-1506715724.jpg"
              alt="user"
            />
          </div>
          <div className="data">
            <UnderlinedLabeledTextField label="First name" text={user.firstName} />
            <UnderlinedLabeledTextField label="Last name" text={user.lastName} />
            <UnderlinedLabeledTextField label="Index" text={user.index} />
          </div>
        </div>
        <div className="block fields-of-study">
          <h2>Fields of study</h2>
          <Table
            size="small"
            pagination={false}
            columns={[
              { title: "Name", dataIndex: "name" },
              { title: "Grade", dataIndex: "grade" },
            ]}
            dataSource={user.specialties.map(s => {
              return {
                key: s.id,
                name: s.name,
                grade: s.grade,
              };
            })}
          />
        </div>
      </div>
      <div className="block utility-section">
        <div className="button">
          <h1 className="text">NOTES</h1>
        </div>
        <div className="button">
          <h1 className="text">PLANS</h1>
        </div>
      </div>
    </div>
  );
};

export default ProfilePage;
