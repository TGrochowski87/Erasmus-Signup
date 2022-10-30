interface Props {
  stringToDisplay: string;
}

const Displayer = ({ stringToDisplay }: Props) => {
  return (
    <div style={{ width: "100px", height: "100px", border: "2px dashed pink" }}>
      <p>{stringToDisplay}</p>
    </div>
  );
};

export default Displayer;
