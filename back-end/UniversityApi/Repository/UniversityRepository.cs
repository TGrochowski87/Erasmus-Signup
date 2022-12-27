using Microsoft.EntityFrameworkCore;
using UniversityApi.DbModels;
using UniversityApi.Models;

namespace UniversityApi.Repository
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly universitydbContext _context;

        public UniversityRepository(universitydbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DestSpeciality>> GetListAsync(DestinationCriteria criteria)
        {
            var list = await _context.DestSpecialities
                .Include(x => x.DestUniversityCodeNavigation)
                .Include(x => x.StudyArea)
                .Include(x => x.ContractDetails)
                .Include(x => x.MinGradeHistories)
                .Include(x => x.SubjectLanguage)
                .Where(x => string.IsNullOrEmpty(criteria.Country) || x.DestUniversityCodeNavigation.Country == criteria.Country)
                .Where(x=> string.IsNullOrEmpty(criteria.SubjectArea) || x.StudyArea.StudyDomain == criteria.SubjectArea)
                .ToListAsync();

            switch (criteria.OrderByInterestedStudents)
            {
                case "asc":
                    return list.OrderBy(x=>x.InterestedStudents);
                case "desc":
                    return list.OrderByDescending(x => x.InterestedStudents);
                default:
                    return list;
            }
        }

        public async Task<University> GetAsync(short destId)
        {
            return await _context.Universities
                .Include(x => x.DestSpecialities).ThenInclude(x => x.StudyArea)
                .Include(x => x.DestSpecialities).ThenInclude(x => x.ContractDetails)
                .Include(x => x.DestSpecialities).ThenInclude(x => x.MinGradeHistories)
                .Include(x => x.DestSpecialities).ThenInclude(x => x.SubjectLanguage)
                .FirstAsync(x => x.DestSpecialities.Any(x => x.Id == destId));
        }

        public IQueryable<DestSpeciality> DestSpecialityGetList()
        {
            var destSpecialityList = _context.DestSpecialities
                 .Include(x => x.DestUniversityCodeNavigation)
                 .Include(x => x.StudyArea)
                 .Include(x => x.ContractDetails)
                 .Include(x => x.MinGradeHistories)
                 .Include(x => x.SubjectLanguage);

            return destSpecialityList;
        }

        public async Task UpdateInterestedStudentsCountAsync(int id, bool increment)
        {
            var speciality = await _context.DestSpecialities
                .SingleAsync(speciality => speciality.Id == id);

            speciality.InterestedStudents += increment ? 1 : -1;
            await _context.SaveChangesAsync();
        }
    }
}
