import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import {
  getCountries,
  getDestinations,
  getDestinationsRecommended,
  getDestinationsRecommendedByStudents,
  getStudyAreas,
  getStudyDomains,
} from "api/universityApi";
import DestFiltering from "models/DestFiltering";
import DestinationList from "models/DestinationList";
import DestSpecialty from "models/DestSpecialty";
import StudyArea from "models/StudyArea";
import StudyDomain from "models/StudyDomain";

interface State {
  studyAreas: StudyArea[];
  studyDomains: StudyDomain[];
  countries: string[];
  destinationList: DestinationList;
  destinationsRecommended: DestSpecialty[] | undefined;
  destinationsRecommendedByStudents: DestSpecialty[] | undefined;
}

const initialState: State = {
  studyDomains: [],
  studyAreas: [],
  countries: [],
  destinationList: {
    totalRows: 0,
    destinations: [],
  },
  destinationsRecommended: undefined,
  destinationsRecommendedByStudents: undefined,
};

export const fetchStudyDomains = createAsyncThunk<StudyDomain[]>("study_domains", async () => {
  const response = await getStudyDomains();
  return response;
});

export const fetchStudyAreas = createAsyncThunk<StudyArea[]>("study_areas", async () => {
  const response = await getStudyAreas();
  return response;
});

export const fetchCountries = createAsyncThunk<string[]>("countries", async () => {
  const response = await getCountries();
  return response;
});

export const fetchDestinations = createAsyncThunk<
  DestinationList,
  { pageSize: number; page: number; filters?: DestFiltering }
>("destinations", async ({ page, pageSize, filters }) => {
  const response = await getDestinations(page, pageSize, filters);
  return response;
});

export const fetchDestinationsRecommended = createAsyncThunk<DestSpecialty[]>("destinations_recommended", async () => {
  const response = await getDestinationsRecommended();
  return response;
});

export const fetchDestinationsRecommendedByStudents = createAsyncThunk<DestSpecialty[]>(
  "destinations_recommended_by_students",
  async () => {
    const response = await getDestinationsRecommendedByStudents();
    return response;
  }
);

const universitySlice = createSlice({
  name: "university",
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder
      .addCase(fetchStudyDomains.fulfilled, (state, action) => {
        state.studyDomains = action.payload;
      })
      .addCase(fetchStudyAreas.fulfilled, (state, action) => {
        state.studyAreas = action.payload;
      })
      .addCase(fetchCountries.fulfilled, (state, action) => {
        state.countries = action.payload;
      })
      .addCase(fetchDestinations.fulfilled, (state, action) => {
        state.destinationList = action.payload;
      })
      .addCase(fetchDestinationsRecommended.fulfilled, (state, action) => {
        state.destinationsRecommended = action.payload;
      })
      .addCase(fetchDestinationsRecommended.rejected, state => {
        state.destinationsRecommended = undefined;
      })
      .addCase(fetchDestinationsRecommendedByStudents.fulfilled, (state, action) => {
        state.destinationsRecommendedByStudents = action.payload;
      })
      .addCase(fetchDestinationsRecommendedByStudents.rejected, state => {
        state.destinationsRecommendedByStudents = undefined;
      });
  },
});
export default universitySlice.reducer;
