import SomeForm from "./SomeForm";
import ExampleContext from "storage/context/exampleContext";

const ExamplePage = () => {
  const newContextValues = {
    stringValue: "Uptown girl!",
    methodValue: () => {
      return "She's been living in her uptown world";
    },
  };

  return (
    <div>
      <h1>The Example Page</h1>
      <button>Click me 'cause why not</button>
      <ExampleContext.Provider value={newContextValues}>
        <SomeForm />
      </ExampleContext.Provider>
    </div>
  );
};

export default ExamplePage;
