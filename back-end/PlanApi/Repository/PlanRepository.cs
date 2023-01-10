using Microsoft.EntityFrameworkCore;
using PlanApi.DbModels;

namespace PlanApi.Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly PlandbContext _context;

        public PlanRepository(PlandbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plan>> GetPlansAsync(long studentId)
        {
            return await _context.Plans
                .Where(plan => plan.StudentId == studentId)
                .Include(plan => plan.Subjects)
                .ThenInclude(subject => subject.MappedSubjectNavigation)
                .ToListAsync();
        }

        public async Task<int> CreatePlanAsync(Plan plan)
        {
            await _context.Plans.AddAsync(plan);
            await _context.SaveChangesAsync();
            var planId = plan.Id;

            var homeSubjects = await _context.HomeSubjects
                .Where(x => x.StudentId == plan.StudentId).ToListAsync();
            var subjects = homeSubjects.Select(homeSubject =>
            {
                return new Subject
                {
                    PlanId = planId,
                    MappedSubject = homeSubject.Id,
                    Name = "",
                    Ects = 0
                };
            });
            _context.Subjects.AddRange(subjects);
            await _context.SaveChangesAsync();

            return planId;
        }

        public async Task UpdatePlanAsync(Plan plan)
        {
            var old = await _context.Plans.FindAsync(plan.Id);
            old.Name = plan.Name;
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePlanSubjects(int planId,
            IEnumerable<Subject> subjects)
        {
            var planSubjects = await _context.Subjects
                .Where(x => x.PlanId == planId).ToListAsync();

            foreach (var old in planSubjects)
            {
                foreach (var newSubject in subjects)
                {
                    if (old.MappedSubject == newSubject.MappedSubject)
                    {
                        old.Name = newSubject.Name;
                        old.Ects = newSubject.Ects;
                        break;
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeletePlanAsync(int id)
        {
            var plan = _context.Plans.Find(id);
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HomeSubject>> GetUserPlanAsync(long studentId)
        {
            return await _context.HomeSubjects
                .Where(homeSubject => homeSubject.StudentId == studentId)
                .Include(homeSubject => homeSubject.Subjects)
                .ToListAsync();
        }

        public async Task CreateUserPlanAsync(long studentId,
            IEnumerable<HomeSubject> subjects)
        {
            var old = _context.HomeSubjects.Where(x => x.StudentId == studentId);
            _context.HomeSubjects.RemoveRange(old);
            await _context.HomeSubjects.AddRangeAsync(subjects);
            await _context.SaveChangesAsync();
        }
    }
}
