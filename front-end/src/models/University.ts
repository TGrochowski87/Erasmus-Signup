interface University {
  id: number;
  subjectAreaName: string;
  subjectAreaId: number;
  universityName: string;
  country: string;
  flagUrl: string;
  website: string | null;
  availablePlaces: number;
  opinionsAmount: number;
  lastYearGradeAvg: number;
  rating: number;
  isObserved: boolean;
}

export default University;
