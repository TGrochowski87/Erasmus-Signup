﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<DestSpeciality>> GetListAsync()
        {
            return await _context.DestSpecialities
                .Include(x => x.DestUniversityCodeNavigation)
                .Include(x => x.StudyArea)
                .Include(x => x.ContractDetails)
                .Include(x => x.MinGradeHistories)
                .Include(x => x.SubjectLanguage).ToListAsync();
        }

        public async Task<University> GetAsync(short destId)
        {
            return await _context.Universities
                .Include(x => x.DestSpecialities).ThenInclude(x=>x.StudyArea)
                .Include(x => x.DestSpecialities).ThenInclude(x=>x.ContractDetails)
                .Include(x => x.DestSpecialities).ThenInclude(x=>x.MinGradeHistories)
                .Include(x => x.DestSpecialities).ThenInclude(x=>x.SubjectLanguage)
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
