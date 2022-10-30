import { createContext } from "react";

const ExampleContext = createContext({
  stringValue: "some initial string value",
  methodValue: (num: number): string => {
    return "something";
  },
});

export default ExampleContext;
