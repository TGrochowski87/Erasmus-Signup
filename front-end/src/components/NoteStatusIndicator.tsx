import { ContainerFilled, ContainerOutlined } from "@ant-design/icons";

interface Props {
  active: boolean;
}

const NoteStatusIndicator = ({ active }: Props) => {
  return active ? (
    <ContainerFilled
      style={{
        fontSize: "1.5rem",
        color: "yellow",
      }}
    />
  ) : (
    <ContainerOutlined
      style={{
        fontSize: "1.5rem",
      }}
    />
  );
};

export default NoteStatusIndicator;
