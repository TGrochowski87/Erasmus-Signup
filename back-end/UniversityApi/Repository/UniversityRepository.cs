using Microsoft.EntityFrameworkCore;
using UniversityApi.DbModels;

namespace UniversityApi.Repository
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly universitydbContext _context;

        public UniversityRepository(universitydbContext context)
        {
            _context = context;
        }

        public IQueryable<DestSpeciality> DestSpecialityGetList()
        {
            var destSpecialityList = _context.DestSpecialities
                 .Include(x => x.DestUniversityCodeNavigation)
                 .Include(x => x.StudyArea)
                 .Include(x => x.MinGradeHistories)
                 .Include(x => x.SubjectLanguage);

            return destSpecialityList;
        }
    }
}
