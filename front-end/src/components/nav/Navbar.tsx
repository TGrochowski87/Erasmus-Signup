// React
import { MouseEventHandler, useEffect, useState } from "react";
import { useLocation, useSearchParams } from "react-router-dom";
// Styles
import "./Navbar.scss";
// Assets
import pwrlogo from "assets/pwr.webp";
import eulogo from "assets/erasmus.webp";
// Components
import NavbarLink from "./NavbarLink";
import { useAppDispatch, useAppSelector } from "storage/redux/hooks";
import { RootState } from "storage/redux/store";
import { fetchOAuthUrl, logIn } from "storage/redux/loginSlice";
import RequestStatus from "storage/redux/RequestStatus";
import FullViewLoading from "components/FullViewLoading";
import AccessTokenRequestDto from "api/DTOs/AccessTokenRequestDto";

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
      path: "home",
    },
    {
      id: 1,
      text: "Destinations",
      path: "list",
    },
    {
      id: 2,
      text: "Log in",
      customOnClick: (event) => {
        event.preventDefault();
        dispatch(fetchOAuthUrl());
      },
    },
  ]);
  const [searchParams] = useSearchParams();

  useEffect(() => {
    const oAuthToken = searchParams.get("oauth_token");
    const oAuthVerifier = searchParams.get("oauth_verifier");

    if (oAuthToken && oAuthVerifier) {
      dispatch(logIn({ oAuthToken, oAuthVerifier }));
    }
  }, [searchParams]);

  useEffect(() => {
    if (userLoggedIn) {
      let linksTemp = [...links];
      linksTemp.splice(linksTemp.length - 1, 1, {
        id: linksTemp.length - 1,
        text: "Profile",
        path: "profile",
      });
      setLinks(linksTemp);
    }
  }, [userLoggedIn]);

  useEffect(() => {
    const currentLocation = pathname.slice(1);
    if (currentLocation === "") {
      return;
    }

    let matchingPaths = links.filter(
      (l) => l.path && currentLocation.includes(l.path)
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
            path={`/${link.path}`}
            active={link.id === activeId && link.path !== undefined}
            customOnClick={link.customOnClick}
          />
        ))}
      </nav>
    </>
  );
};

export default Navbar;
