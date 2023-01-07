using Microsoft.EntityFrameworkCore;
using UniversityApi.DbModels;
using UniversityApi.Models;

namespace UniversityApi.Repository
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly UniversitydbContext _context;

        public UniversityRepository(UniversitydbContext context)
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
                .Where(x=> string.IsNullOrEmpty(criteria.SubjectAreaId) || x.StudyArea.Id == criteria.SubjectAreaId)
                .Where(x=> string.IsNullOrEmpty(criteria.UniversityName) || x.DestUniversityCodeNavigation.Name.Contains(criteria.UniversityName))
                .ToListAsync();

            switch (criteria.OrderBy)
            {
                case "InterestedStudentsAsc":
                    return list.OrderBy(x => x.InterestedStudents);
                case "InterestedStudentsDesc":
                    return list.OrderByDescending(x => x.InterestedStudents);
                case "AverageAsc":
                    return list.OrderBy(x => x.MinGradeHistories.FirstOrDefault()?.Grade);
                case "AverageDesc":
                    return list.OrderByDescending(x => x.MinGradeHistories.FirstOrDefault()?.Grade);
                default:
                    return list;
            }
        }

        public async Task<IEnumerable<DestSpeciality>> GetListRecommendedDestinationsAsync(short? studyDomainId, float? averageGrade)
        {
            var list = await _context.DestSpecialities
                .Include(x => x.DestUniversityCodeNavigation)
                .Include(x => x.StudyArea)
                .Include(x => x.ContractDetails)
                .Include(x => x.MinGradeHistories)
                .Include(x => x.SubjectLanguage)
                .Where(x => studyDomainId == null || x.StudyArea.StudyDomainId == studyDomainId)
                .Where(x => averageGrade == null ||
                averageGrade < 2 ||
                !x.MinGradeHistories.FirstOrDefault().Grade.HasValue ||
                x.MinGradeHistories.FirstOrDefault().Grade <= averageGrade)
                .ToListAsync();

            return list;
        }

        public async Task<DestSpeciality> GetRecommendedByStudentsDestinationsAsync(short destId)
        {
            var dest = await _context.DestSpecialities
                .Include(x => x.DestUniversityCodeNavigation)
                .Include(x => x.StudyArea)
                .Include(x => x.ContractDetails)
                .Include(x => x.MinGradeHistories)
                .Include(x => x.SubjectLanguage)
                .FirstOrDefaultAsync(x => x.Id == destId);

            return dest;
        }

        public async Task<IEnumerable<StudyDomain>> GetStudyDomainListAsync()
        {
            return await _context.StudyDomains.ToListAsync();
        }

        public async Task<IEnumerable<StudyArea>> GetStudyAreaListAsync()
        {
            return await _context.StudyAreas.ToListAsync();
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
