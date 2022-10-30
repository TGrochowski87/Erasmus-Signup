import { useContext, useState } from "react";
import ExampleContext from "storage/context/exampleContext";
import Displayer from "./Displayer";

const SomeForm = () => {
  const [text, setText] = useState<string>("");

  const { stringValue } = useContext(ExampleContext);

  return (
    <>
      <form>
        <label htmlFor="one">Some input here</label>
        <input
          id="one"
          type="text"
          value={text}
          onChange={(event) => setText(event.target.value)}
        />
      </form>
      <Displayer stringToDisplay={stringValue} />
    </>
  );
};

export default SomeForm;
