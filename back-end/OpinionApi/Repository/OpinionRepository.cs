using Microsoft.EntityFrameworkCore;
using OpinionApi.DbModels;

namespace OpinionApi.Repository
{
    public class OpinionRepository : IOpinionRepository
    {
        private readonly OpiniondbContext _context;

        public OpinionRepository(OpiniondbContext context)
        {
            _context = context;
        }


        public async Task<int> CreateAsync(Opinion opinion)
        {
            await _context.Opinions.AddAsync(opinion);
            await _context.SaveChangesAsync();

            return opinion.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Opinions.FindAsync(id);
            if (item != null)
            {
                _context.Opinions.Remove(item);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task EditAsync(Opinion opinion)
        {
            _context.Attach(opinion);
            _context.Entry(opinion).Property("Content").IsModified = true;
            _context.Entry(opinion).Property("Rating").IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Opinion>> GetListAsync(int specId)
        {
            return await _context.Opinions.Where(x=>x.SpecialityId == specId).ToListAsync();
        }

    }
}
