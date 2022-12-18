// Ant Design
import { HeartFilled, HeartOutlined } from "@ant-design/icons";

interface Props {
  active: boolean;
}

const FavoriteStatusIndicator = ({ active }: Props) => {
  return active ? (
    <HeartFilled
      style={{
        fontSize: "1.5rem",
        color: "red",
      }}
    />
  ) : (
    <HeartOutlined
      style={{
        fontSize: "1.5rem",
      }}
    />
  );
};

export default FavoriteStatusIndicator;
