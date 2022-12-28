using UserApi.DbModels;

namespace UserApi.Repository
{
    public interface IUserRepository
    {
        Task ProfileEditAsync(UserProfile student);
        Task<UserProfile> GetProfileAsync(int userId);
    }
}
