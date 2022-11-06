import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getUniversities } from "api/universityApi";
import University from "models/University";
import RequestStatus from "./RequestStatus";

interface State {
  value: University[];
  status: RequestStatus;
}

const initialState: State = {
  value: [],
  status: RequestStatus.idle,
};

export const fetchUniversities = createAsyncThunk("universities", async () => {
  //TODO: Add error handling
  const response = await getUniversities();
  return response;
});

const universitySlice = createSlice({
  name: "university",
  initialState,
  reducers: {
    test: (state) => {
      state.value = [];
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchUniversities.pending, (state) => {
        state.status = RequestStatus.loading;
      })
      .addCase(fetchUniversities.fulfilled, (state, action) => {
        state.status = RequestStatus.idle;
        state.value = action.payload;
      })
      .addCase(fetchUniversities.rejected, (state) => {
        state.status = RequestStatus.failed;
      });
  },
});

export default universitySlice.reducer;
