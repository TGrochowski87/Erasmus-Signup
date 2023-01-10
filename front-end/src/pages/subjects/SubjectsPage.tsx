// Ant Design
import { Button, InputNumber, List } from "antd";
import Input from "antd/lib/input/Input";
// Components
import HomeSubject from "models/HomeSubject";
// Styles
import "./SubjectsPage.scss";

interface Props {
  subjects: HomeSubject[];
  setSubjects: React.Dispatch<React.SetStateAction<HomeSubject[]>>;
}

const SubjectsPage = ({ subjects, setSubjects }: Props) => {
  return (
    <div id="subjects-page-container">
      <div id="plan-list" className="block">
        <div id="plan-headder">
          <h1>Subjects:</h1>
          <Button size="large" type="primary" onClick={undefined}>
            Save changes
          </Button>
          <Button
            size="large"
            type="primary"
            onClick={() => {
              setSubjects(prevState => {
                const newSubjects = [...prevState];
                newSubjects.push({
                  id: -1,
                  name: "",
                  ects: 0,
                });
                return newSubjects;
              });
            }}>
            Add new subject
          </Button>
        </div>

        <List
          dataSource={subjects}
          renderItem={item => (
            <List.Item className="plan-subject-row" key={item.id}>
              <Input
                className="subject-text-input"
                placeholder="Subject name"
                maxLength={100}
                value={item.name}
                onChange={event => {
                  setSubjects(prevState => {
                    const newSubjects = [...prevState];
                    newSubjects.find(x => {
                      return x.id === item.id;
                    })!.name = event.target.value;
                    return newSubjects;
                  });
                }}
              />
              <InputNumber
                min={1}
                max={30}
                defaultValue={1}
                value={item.ects}
                onChange={event => {
                  setSubjects(prevState => {
                    const newSubjects = [...prevState];
                    newSubjects.find(x => {
                      return x.id === item.id;
                    })!.ects = event!;
                    return newSubjects;
                  });
                }}
              />
              <Button
                className="delete-button"
                size="large"
                type="primary"
                onClick={() => {
                  setSubjects(prevState => {
                    const newSubjects = [
                      ...prevState.filter(x => {
                        return x.id !== item.id;
                      }),
                    ];
                    return newSubjects;
                  });
                }}>
                ✖
              </Button>
            </List.Item>
          )}
        />
      </div>
    </div>
  );
};

export default SubjectsPage;
