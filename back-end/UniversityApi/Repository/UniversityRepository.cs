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

        public async Task<IEnumerable<University>> GetListAsync()
        {
            //return await _context.Universities.ToListAsync();

            var universities = new List<University>();

            return destSpecialityList;
        }
    }
}
