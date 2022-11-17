// Styles
import "./Navbar.scss";
// Assets
import pwrlogo from "assets/pwr.webp";
import eulogo from "assets/erasmus.webp";
import NavbarLink from "./NavbarLink";
import { useLocation } from "react-router-dom";
import { useEffect, useMemo, useState } from "react";

interface NavLinkData {
  id: number;
  text: string;
  path: string;
}

const Navbar = () => {
  const { pathname } = useLocation();
  const [activeId, setActiveId] = useState<number>();
  const links: NavLinkData[] = useMemo<NavLinkData[]>(
    () => [
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
        text: "Profile",
        path: "/profile",
      },
    ],
    []
  );

  useEffect(() => {
    let matchingPaths = links.filter((l) => pathname.includes(l.path));
    if (matchingPaths.length > 1) {
      matchingPaths = matchingPaths.filter((p) => p.id !== 0);
    }

    if (matchingPaths.length !== 1) {
      throw new Error("Cannot determine a single matching path.");
    }

    setActiveId(matchingPaths[0].id);
  }, [pathname, links]);

  return (
    <nav className="nav">
      <img src={pwrlogo} alt="pwr logo" />
      <img src={eulogo} alt="eu logo" style={{ marginRight: "auto" }} />
      {links.map((link) => (
        <NavbarLink
          key={link.id}
          text={link.text}
          path={link.path}
          active={link.id === activeId}
        />
      ))}
      {/* <Button
          onClick={() => {
            navigate("/");
          }}
        >
          Home
        </Button>
        <Button
          onClick={() => {
            navigate("/list");
          }}
        >
          Destinations
        </Button>
        <Button
          onClick={() => {
            navigate("/profile");
          }}
        >
          Profile
        </Button> */}
    </nav>
  );
};

export default Navbar;
