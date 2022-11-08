using NoteApi.DbModels;

namespace NoteApi.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly NoteDataContext _context;

        public NoteRepository(NoteDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetList()
        {
            var notes = new List<Note>();

            notes.Add(
                new Note() 
                { 
                    Id = 1,
                    Content = "la fac"
                });

            notes.Add(
                new Note()
                {
                    Id = 2,
                    Content = "ok"
                });

            return notes;
        }

        public async Task Add(Note note)
        {
           // await _context.AddAsync(note);
        }
    }
}
