import { Image, Table } from "antd";
import UnderlinedLabeledTextField from "components/UnderlinedLabeledTextField";
import { User } from "models/User";
// Styles
import "./ProfilePage.scss";

interface Props {
  user: User;
  navigateToNotesPage: () => void;
}

const ProfilePage = ({ user, navigateToNotesPage }: Props) => {
  return (
    <div className="user-page">
      <div className="user-section">
        <div className="block personal-data">
          <div className="image-space">
            <Image
              width={200}
              height={250}
              src={user.photoUtl_400x500}
              alt="user"
            />
          </div>
          <div className="data">
            <UnderlinedLabeledTextField label="Full name" text={user.titlesBefore + " " + user.firstName + " " + user.middleNames + " " + user.lastName + " " + user.titlesAfter} />
            <UnderlinedLabeledTextField label="E-Mail" text={user.email} />
            <UnderlinedLabeledTextField label="Role" text={ user.isStaff ? "Staff" : "Student" }/>
            { user.studentNumber ? <UnderlinedLabeledTextField label="Student number" text={user.studentNumber} /> : ""}

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
          />
        </div>
      </div>
      <div className="block utility-section">
        <div className="button" onClick={() => navigateToNotesPage()}>
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
