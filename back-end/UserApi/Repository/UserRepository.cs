using Microsoft.EntityFrameworkCore;
using UserApi.DbModels;

namespace UserApi.Repository
{
    public partial class UserRepository : IUserRepository
    {
        private readonly UserdbContext _context;

        public UserRepository(UserdbContext context)
        {
            _context = context;
        }

        public async Task ProfileEditAsync(UserProfile profile)
        {
            var profileGet = _context.UserProfiles.FirstOrDefault(x => x.UserId == profile.UserId);

            if(profileGet != null)
            {
                profileGet.AverageGrade = profile.AverageGrade;
                profileGet.PreferencedStudyDomainId = profile.PreferencedStudyDomainId;
                _context.Attach(profileGet);
                _context.Entry(profileGet).Property("PreferencedStudyDomainId").IsModified = true;
                _context.Entry(profileGet).Property("AverageGrade").IsModified = true;

                await _context.SaveChangesAsync();

            }
            else
            {
                await _context.UserProfiles.AddAsync(profile);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserProfile> GetProfileAsync(int userId)
        {
            return await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<UserProfile>> GetUsersByStudyDomain(short studyDomainId)
        {
            return await _context.UserProfiles.Where(x => x.PreferencedStudyDomainId == studyDomainId).ToListAsync();
        }

    }
}
