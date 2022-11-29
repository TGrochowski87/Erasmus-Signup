// React
import { MouseEventHandler, useEffect, useMemo, useState } from "react";
import { useLocation } from "react-router-dom";
// Styles
import "./Navbar.scss";
// Assets
import pwrlogo from "assets/pwr.webp";
import eulogo from "assets/erasmus.webp";
// Components
import NavbarLink from "./NavbarLink";
import { useAppDispatch, useAppSelector } from "storage/redux/hooks";
import { RootState } from "storage/redux/store";
import { logIn } from "storage/redux/loginSlice";
import RequestStatus from "storage/redux/RequestStatus";
import FullViewLoading from "components/FullViewLoading";

interface NavLinkData {
  id: number;
  text: string;
  path?: string;
  customOnClick?: MouseEventHandler<HTMLAnchorElement>;
}

const Navbar = () => {
  const { userLoggedIn, status } = useAppSelector(
    (state: RootState) => state.login
  );
  const dispatch = useAppDispatch();
  const { pathname } = useLocation();
  const [activeId, setActiveId] = useState<number>();
  const [links, setLinks] = useState<NavLinkData[]>([
    {
      id: 0,
      text: "Home",
      path: "/",
    },
    {
      id: 1,
      text: "Destinations",
      path: "/list",
    },
    {
      id: 2,
      text: "Log in",
      customOnClick: (event) => {
        event.preventDefault();
        dispatch(logIn());
      },
    },
  ]);

  useEffect(() => {
    if (userLoggedIn) {
      setLinks((prevState) =>
        prevState.splice(prevState.length - 1, 1, {
          id: prevState.length - 1,
          text: "Profile",
          path: "/profile",
        })
      );
    }
  }, [userLoggedIn]);

  useEffect(() => {
    let matchingPaths = links.filter(
      (l) => l.path && pathname.includes(l.path)
    );
    if (matchingPaths.length > 1) {
      matchingPaths = matchingPaths.filter((p) => p.id !== 0);
    }

    if (matchingPaths.length !== 1) {
      throw new Error("Cannot determine a single matching path.");
    }

    setActiveId(matchingPaths[0].id);
  }, [pathname, links]);

  return (
    <>
      {status === RequestStatus.loading && <FullViewLoading />}
      <nav className="nav">
        <img src={pwrlogo} alt="pwr logo" />
        <img src={eulogo} alt="eu logo" style={{ marginRight: "auto" }} />
        {links.map((link) => (
          <NavbarLink
            key={link.id}
            text={link.text}
            path={link.path}
            active={link.id === activeId && link.path !== undefined}
            customOnClick={link.customOnClick}
          />
        ))}
      </nav>
    </>
  );
};

export default Navbar;
