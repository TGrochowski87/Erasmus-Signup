using UniversityApi.DbModels;

namespace UniversityApi.Repository
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly UniversityDataContext _context;

        public UniversityRepository(UniversityDataContext context)
        {
            _context = context;
        }

        public IEnumerable<University> GetList()
        {
            var universities = new List<University>();

            universities.Add(
                new University() 
                { 
                    Id = 1,
                    UniversityName = "Spain University",
                    SubjectAreaName = "Literature and linguistics",
                    SubjectAreaId = 342,
                    Country = "Spain",
                    FlagUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Flag_of_Spain.svg/1280px-Flag_of_Spain.svg.png",
                    Website = "https://pl.wikipedia.org/wiki/Flaga_Hiszpanii",
                    AvailablePlaces = 3,
                    Rating = 4.5M,
                    IsObserved = true 
                });

            universities.Add(
                new University()
                {
                    Id = 1,
                    UniversityName = "Spain University",
                    SubjectAreaName = "Literature and linguistics2",
                    SubjectAreaId = 342,
                    Country = "Spain2",
                    FlagUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9a/Flag_of_Spain.svg/1280px-Flag_of_Spain.svg.png",
                    Website = "https://pl.wikipedia.org/wiki/Flaga_Hiszpanii",
                    AvailablePlaces = 10,
                    Rating = 2.5M,
                    IsObserved = false
                });

            return universities;
        }
    }
}
