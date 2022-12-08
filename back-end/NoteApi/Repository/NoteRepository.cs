using Microsoft.EntityFrameworkCore;
using NoteApi.Models;
using NoteApi.DbModels;

namespace NoteApi.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly notedbContext _notedbContext;

        public NoteRepository(notedbContext notedbContext)
        {
            _notedbContext = notedbContext;
        }

        public async Task<IEnumerable<PlanNote>> GetPlanNotesAsync()
        {
            return await _notedbContext.PlanNotes
                .Include(note => note.Note).ToListAsync();
        }

        public async Task<IEnumerable<PlanNote>> GetPlanNotesAsync(int userId)
        {
            return await _notedbContext.PlanNotes
                .Where(n => n.Note.UserId == userId)
                .Include(note => note.Note).ToListAsync();
        }

        public async Task<IEnumerable<SpecialityNote>> GetSpecialityNotesAsync()
        {
            return await _notedbContext.SpecialityNotes
                .Include(note => note.Note).ToListAsync();
        }

        public async Task<IEnumerable<SpecialityNote>> GetSpecialityNotesAsync(int userId)
        {
            return await _notedbContext.SpecialityNotes
                .Where(n => n.Note.UserId == userId)
                .Include(note => note.Note).ToListAsync();
        }

        public async Task<IEnumerable<SpecialityHighlightNote>> GetSpecialityHighlightNotesAsync()
        {
            return await _notedbContext.SpecialityHighlightNotes
                .Include(note => note.Note).ToListAsync();
        }

        public async Task<IEnumerable<SpecialityHighlightNote>> GetSpecialityHighlightNotesAsync(int userId)
        {
            return await _notedbContext.SpecialityHighlightNotes
                .Where(n => n.Note.UserId == userId)
                .Include(note => note.Note).ToListAsync();
        }

        public async Task<IEnumerable<SpecialityPriorityNote>> GetSpecialityPriorityNotesAsync()
        {
            return await _notedbContext.SpecialityPriorityNotes
                .Include(note => note.Note).ToListAsync();
        }

        public async Task<IEnumerable<SpecialityPriorityNote>> GetSpecialityPriorityNotesAsync(int userId)
        {
            return await _notedbContext.SpecialityPriorityNotes
                .Where(n => n.Note.UserId == userId)
                .Include(note => note.Note).ToListAsync();
        }

        public async Task<int> AddPlanNoteAsync(PlanNoteVM noteVm)
        {
            return await AddNoteAsync(noteVm.UserId, (note) =>
            {
                return new PlanNote
                {
                    NoteId = note.Id,
                    PlanId = noteVm.PlanId,
                    Content = noteVm.Content
                };
            });
        }

        public async Task<int> AddSpecialityNoteAsync(SpecialityNoteVM noteVm)
        {
            return await AddNoteAsync(noteVm.UserId, (note) =>
            {
                return new SpecialityNote
                {
                    NoteId = note.Id,
                    Content = noteVm.Content,
                    SpecialityId = noteVm.SpecialityId
                };
            });
        }

        public async Task<int> AddSpecialityPriorityNoteAsync(SpecialityPriorityNoteVM noteVm)
        {
            return await AddNoteAsync(noteVm.UserId, (note) =>
            {
                return new SpecialityPriorityNote
                {
                    NoteId = note.Id,
                    SpecialityId = noteVm.SpecialityId,
                    Priority = noteVm.Priority
                };
            });
        }

        public async Task<int> AddSpecialityHighlightNoteAsync(SpecialityHighlightNoteVM noteVm)
        {
            return await AddNoteAsync(noteVm.UserId, (note) =>
            {
                return new SpecialityHighlightNote
                {
                    NoteId = note.Id,
                    SpecialityId = noteVm.SpecialityId,
                    Positive = noteVm.Positive
                };
            });
        }

        private async Task<int> AddNoteAsync<T>(int userId, Func<Note, T> Provider)
            where T : class
        {
            var note = new Note
            {
                UserId = userId,
                CreatedAt = DateTime.Now
            };

            await _notedbContext.AddAsync(note);
            await _notedbContext.SaveChangesAsync();

            await _notedbContext.AddAsync<T>(Provider(note));
            await _notedbContext.SaveChangesAsync();

            return note.Id;
        }
    }
}
