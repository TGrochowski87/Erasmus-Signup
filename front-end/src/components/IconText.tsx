import { Space } from "antd";
import React from "react";

interface Props {
  icon: React.FC;
  text: string | number;
}

const IconText = ({ icon, text }: Props) => {
  return (
    <Space>
      {React.createElement(icon)}
      {text.toString()}
    </Space>
  );
};

export default IconText;
