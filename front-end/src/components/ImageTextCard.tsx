// Styles
import "./Components.scss";

interface Props {
  children: React.ReactNode;
}

const ImageTextCard = ({ children }: Props) => {
  return <div className="card">{children}</div>;
};

//
// {Children.map(Children.toArray(children), (child, index) => {
//   if (index === 0) {
//     return <>{child}</>;
//   }
//   return (
//     <>
//       <hr />
//       {child}
//     </>
//   );
// })}

export default ImageTextCard;
