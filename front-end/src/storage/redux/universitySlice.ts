import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { getStudyDomains } from "api/universityApi";
import DestinationLists from "models/DestinationList";
import StudyDomain from "models/StudyDomain";

interface State {
  loading: boolean;
  studyDomains: StudyDomain[];
  destinationLists: DestinationLists;
}

const initialState: State = {
  loading: false,
  studyDomains: [],
  destinationLists: {
    totalRows: 0,
    destinations: [],
    recomendedDestinations: [],
    recommendedByStudentsDestinations: [],
  },
};

export const fetchStudyDomains = createAsyncThunk<StudyDomain[]>("fields_of_study", async () => {
  const response = await getStudyDomains();
  return response;
});

// export const fetchAllDestinationLists = createAsyncThunk<DestinationLists>("destinations_lists", async () => {
//   const response = await getDestinations();
// })

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
