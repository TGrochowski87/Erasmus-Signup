// Ant Design
import { Button, InputNumber, List } from "antd";
import Input from "antd/lib/input/Input";
// Components
import Plan from "models/Plan";
// Styles
import "./PlansPage.scss";

interface Props {
  plans: Plan[];
  currentPlan: Plan | undefined;
  isCoordinator: boolean;
  setCurrentPlan: React.Dispatch<React.SetStateAction<Plan | undefined>>;
  loadPlan: (id: string) => void;
}

const PlansPage = ({ plans, currentPlan, setCurrentPlan, loadPlan, isCoordinator }: Props) => {
  return (
    <div id="plan-page-container">
      <div id="plan-list" className="block">
        <h1>Plans:</h1>
        <List
          dataSource={plans}
          renderItem={item => (
            <List.Item
              onClick={() => {
                loadPlan(item.id.toString());
              }}
              style={{ display: "block", margin: "0" }}
              key={item.id}>
              <h2 style={{ margin: "0" }}>{item.name}</h2>
              <p style={{ margin: "0 0.5rem" }}>{item.specialtyId}</p>
            </List.Item>
          )}
        />
      </div>
      <div id="plan-subjects-panel" className="block">
        {currentPlan ? (
          <div id="plan" className="block ">
            <div id="plan-headder">
              <Input
                className="plan-name-input"
                placeholder="(untitled plan)"
                maxLength={100}
                value={currentPlan.name}
                onChange={event => {
                  setCurrentPlan(prevState => {
                    const newPlan: Plan = {
                      ...prevState!,
                      name: event.target.value,
                      planSubjects: [...prevState!.planSubjects!],
                    };

                    return newPlan;
                  });
                }}
              />
              {isCoordinator ? (
                <div className="plan-buttons-panel">
                  <Button className="accept-button" size="large" type="primary">
                    Accept
                  </Button>
                  <Button className="decline-button" size="large" type="primary">
                    Decline
                  </Button>
                </div>
              ) : (
                <div className="plan-buttons-panel">
                  <Button className="save-button" size="large" type="primary">
                    Save changes
                  </Button>
                  <Button className="send-button" size="large" type="primary">
                    Send to coordinator
                  </Button>
                  <Button
                    className="delete-button"
                    size="large"
                    type="primary"
                    onClick={() => {
                      // TODO handler
                    }}>
                    Delete
                  </Button>
                </div>
              )}
            </div>
            <List
              dataSource={currentPlan.planSubjects}
              renderItem={item => (
                <List.Item className="plan-subject-row" key={item.id}>
                  <div className="plan-subject-home">
                    <h2>{item.homeSubject.name}</h2>
                    <p>ECTS: {item.homeSubject.ects}</p>
                  </div>
                  <div className="plan-subject-destination">
                    <Input
                      className="subject-text-input"
                      placeholder="Subject name"
                      maxLength={100}
                      value={item.name}
                      onChange={event => {
                        setCurrentPlan(prevState => {
                          const newPlan: Plan = { ...prevState!, planSubjects: [...prevState!.planSubjects!] };
                          newPlan.planSubjects!.find(x => {
                            return x.id === item.id;
                          })!.name = event.target.value;
                          // item.name = event.target.value;
                          return newPlan;
                        });
                      }}
                    />
                    <InputNumber
                      min={1}
                      max={30}
                      defaultValue={1}
                      value={item.ects}
                      onChange={event => {
                        setCurrentPlan(prevState => {
                          const newPlan: Plan = { ...prevState!, planSubjects: [...prevState!.planSubjects!] };
                          newPlan.planSubjects!.find(x => {
                            return x.id === item.id;
                          })!.ects = event!;
                          return newPlan;
                        });
                      }}
                    />
                  </div>
                </List.Item>
              )}
            />
            {isCoordinator ? (
              <div className="plan-buttons-panel">
                <Button className="accept-button" size="large" type="primary">
                  Accept
                </Button>
                <Button className="decline-button" size="large" type="primary">
                  Decline
                </Button>
              </div>
            ) : (
              <div className="plan-buttons-panel">
                <Button className="save-button" size="large" type="primary">
                  Save changes
                </Button>
                <Button className="send-button" size="large" type="primary">
                  Send to coordinator
                </Button>
                <Button
                  className="delete-button"
                  size="large"
                  type="primary"
                  onClick={() => {
                    // TODO handler
                  }}>
                  Delete
                </Button>
              </div>
            )}
          </div>
        ) : (
          <div id="select-plan-info">
            <p>Select plan, from the menu on the left</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default PlansPage;
