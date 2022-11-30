using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Note>> GetListAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<int> AddAsync(string Content)
        {
            var note = new Note { Content = Content };
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return note.Id;
        }

        public async Task EditAsync(int Id, string content)
        {
            var note = await _context.Notes.FindAsync(Id);
            if (note == null)
            {
                throw new ArgumentException($"Note with id {Id} not found.");
            }

            note.Content = content;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var note = await _context.Notes.FindAsync(Id);
            if (note == null)
            {
                throw new ArgumentException($"Note with id {Id} not found.");
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}
