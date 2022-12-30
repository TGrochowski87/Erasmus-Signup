import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getStudyDomains } from "api/universityApi";
import StudyDomain from "models/StudyDomain";

interface State {
  loading: boolean;
  studyDomains: StudyDomain[];
}

const initialState: State = {
  loading: false,
  studyDomains: [],
};

export const fetchStudyDomains = createAsyncThunk<StudyDomain[]>("fields_of_study", async () => {
  const response = await getStudyDomains();
  return response;
});

const universitySlice = createSlice({
  name: "university",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(fetchStudyDomains.pending, state => {
        state.loading = true;
      })
      .addCase(fetchStudyDomains.fulfilled, (state, action) => {
        state.loading = false;
        state.studyDomains = action.payload;
      })
      .addCase(fetchStudyDomains.rejected, state => {
        state.loading = false;
      });
  },
});

export default universitySlice.reducer;
