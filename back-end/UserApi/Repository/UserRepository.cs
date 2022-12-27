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

        public async Task StudentEditAsync(Student student)
        {
            var isExists = _context.Students.Any(x => x.UserId == student.UserId);

            if(isExists)
            {
                _context.Attach(student);
                _context.Entry(student).Property("PwrSpeciality").IsModified = true;
                _context.Entry(student).Property("AverageGrade").IsModified = true;

                await _context.SaveChangesAsync();

            }
            else
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student> GetStudentAsync(int userId)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.UserId == userId);
        }

    }
}
