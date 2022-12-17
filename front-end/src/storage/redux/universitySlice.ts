import { createSlice } from "@reduxjs/toolkit";
import RequestStatus from "./RequestStatus";

interface State {
  status: RequestStatus;
}

const initialState: State = {
  status: RequestStatus.idle,
};

const universitySlice = createSlice({
  name: "university",
  initialState,
  reducers: {},
});

export default universitySlice.reducer;
