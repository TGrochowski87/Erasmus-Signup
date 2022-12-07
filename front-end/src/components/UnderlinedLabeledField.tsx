import { Children } from "react";

interface Props {
  label?: string;
  children: React.ReactNode;
}

const UnderlinedLabeledField = ({ label = "", children }: Props) => {
  return (
    <div style={{ width: "80%" }}>
      <div style={{ borderBottom: "1px solid black" }}>
        {Children.only(children)}
      </div>
      <h5
        style={{
          position: "relative",
          float: "right",
          marginRight: "1rem",
          lineHeight: "1.2",
        }}
      >
        {label}
      </h5>
    </div>
  );
};

export default UnderlinedLabeledField;
