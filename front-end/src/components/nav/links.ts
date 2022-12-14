import NavLinkData from "./NavLinkData";

export const anonymousUserLinks = (dispatch: Function, fetchOAuthUrl: Function): NavLinkData[] => [
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
    customOnClick: event => {
      event.preventDefault();
      dispatch(fetchOAuthUrl());
    },
  },
];

export const loggedInUserLinks = (dispatch: Function, logOut: Function): NavLinkData[] => [
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
    text: "Notes",
    path: "notes",
  },
  {
    id: 3,
    text: "Profile",
    path: "profile",
  },
  {
    id: 4,
    text: "Log out",
    customOnClick: event => {
      event.preventDefault();
      dispatch(logOut());
    },
  },
];
