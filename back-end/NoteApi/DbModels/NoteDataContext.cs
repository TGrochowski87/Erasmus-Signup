using Microsoft.EntityFrameworkCore;

namespace NoteApi.DbModels
{
    public class NoteDataContext : DbContext
    {
        public NoteDataContext(DbContextOptions<NoteDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Note> Universities;

    }
}
