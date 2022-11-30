using Microsoft.EntityFrameworkCore;

namespace UniversityApi.DbModels
{
    public class UniversityDataContext : DbContext
    {
        public UniversityDataContext(DbContextOptions<UniversityDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<University> Universities { get; set; }

    }
}
