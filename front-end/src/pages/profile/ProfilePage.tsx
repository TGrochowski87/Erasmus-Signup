import { Image, Table } from "antd";
import UnderlinedLabeledField from "components/UnderlinedLabeledField";
import User from "models/User";
// Styles
import "./ProfilePage.scss";

interface Props {
  user: User;
}

const ProfilePage = ({ user }: Props) => {
  return (
    <div className="user-page">
      <div className="size-box">
        <div className="user-profile">
          <div className="user section">
            <div className="top">
              <Image
                width={160}
                height={180}
                src="https://image.shutterstock.com/image-photo/passport-photo-portrait-middle-aged-260nw-1506715724.jpg"
                alt="user"
              />
              <div className="data">
                <UnderlinedLabeledField
                  label="First name"
                  text={user.firstName}
                />
                <UnderlinedLabeledField
                  label="Last name"
                  text={user.lastName}
                />
                <UnderlinedLabeledField label="Index" text={user.index} />
              </div>
            </div>
            <br />
            <br />
            <hr />
            <div className="bottom">
              <h2>Fields of study</h2>
              <Table
                size="small"
                pagination={false}
                columns={[
                  { title: "Name", dataIndex: "name" },
                  { title: "Grade", dataIndex: "grade" },
                ]}
                dataSource={user.specialties.map((s) => {
                  return {
                    key: s.id,
                    name: s.name,
                    grade: s.grade,
                  };
                })}
              />
            </div>
          </div>
          <div className="utility section"></div>
        </div>
      </div>
    </div>
  );
};

export default ProfilePage;

// Maybe I will come back to it
// Second variant
{
  /* <div className="user-page">
      <div className="section user-data-space">
        <div style={{ maxWidth: "800px", width: "100%" }}>
          <div className="user-card"></div>
        </div>
      </div>
      <div className="section utility-space"></div>
    </div> */
}
