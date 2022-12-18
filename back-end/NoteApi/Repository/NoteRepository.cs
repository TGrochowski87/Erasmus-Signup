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

        public async Task<(int id, int decrement, int increment)> AddSpecialityPriorityNoteAsync(
            SpecialityPriorityNoteVM noteVm)
        {
            var transaction = await _notedbContext.Database.BeginTransactionAsync();
            var (removed, updated) = await UpdateExistingPriorityNotes(noteVm);
            int id = -1;
            if (updated < 0)
            {
                id = await AddNoteAsync(noteVm.UserId, (note) =>
                {
                    return new SpecialityPriorityNote
                    {
                        NoteId = note.Id,
                        SpecialityId = noteVm.SpecialityId,
                        Priority = noteVm.Priority
                    };
                });
                updated = noteVm.SpecialityId;
            }
            transaction.Commit();

            if (removed >= 0 && await IsLiked(removed, noteVm.UserId))
            {
                removed = -1;
            }

            if (id < 0 || await IsLiked(updated, noteVm.UserId))
            {
                updated = -1;
            }

            return (id, removed, updated);
        }

        private async Task<(int removed, int existed)> UpdateExistingPriorityNotes(
            SpecialityPriorityNoteVM noteVm)
        {
            var removed = await RemoveExistingPriority(noteVm);
            var existed = await UpdateRatedSpeciality(noteVm);

            return (removed, existed);
        }

        private async Task<int> RemoveExistingPriority(SpecialityPriorityNoteVM noteVm)
        {
            var priorityToReplace = await (
                from note in _notedbContext.Notes
                join priority in _notedbContext.SpecialityPriorityNotes
                on note.Id equals priority.NoteId
                where note.UserId == noteVm.UserId
                    && priority.Priority == noteVm.Priority
                    && priority.SpecialityId != noteVm.SpecialityId
                select priority).SingleOrDefaultAsync();
            if (priorityToReplace is not null)
            {
                await CascadeDeleteNote(priorityToReplace.NoteId);
                return priorityToReplace.SpecialityId;
            }

            return -1;
        }

        private async Task<int> UpdateRatedSpeciality(SpecialityPriorityNoteVM noteVm)
        {
            var alreadyRated = await (
                from note in _notedbContext.Notes
                join priority in _notedbContext.SpecialityPriorityNotes
                on note.Id equals priority.NoteId
                where note.UserId == noteVm.UserId
                    && priority.SpecialityId == noteVm.SpecialityId
                select priority).Include(n => n.Note).SingleOrDefaultAsync();
            if (alreadyRated is not null && alreadyRated.Priority != noteVm.Priority)
            {
                alreadyRated.Priority = noteVm.Priority;
                alreadyRated.Note.CreatedAt = DateTime.Now;
                await _notedbContext.SaveChangesAsync();
                return alreadyRated.SpecialityId;
            }

            return -1;
        }

        private async Task<bool> IsLiked(int id, int userId)
        {
            var liked = await (
                from note in _notedbContext.Notes
                join highlight in _notedbContext.SpecialityHighlightNotes
                on note.Id equals highlight.NoteId
                where highlight.SpecialityId == id && highlight.Positive && note.UserId == userId
                select highlight).SingleOrDefaultAsync();
            return liked is not null;
        }

        public async Task<(int id, bool decrement, bool increment)> AddSpecialityHighlightNoteAsync(
            SpecialityHighlightNoteVM noteVm)
        {
            var transaction = await _notedbContext.Database.BeginTransactionAsync();
            var (id, existed) = await UpdateExistingHighlightNote(noteVm);
            var modified = id >= 0;
            if (!existed)
            {
                id = await AddNoteAsync(noteVm.UserId, (note) =>
                {
                    return new SpecialityHighlightNote
                    {
                        NoteId = note.Id,
                        SpecialityId = noteVm.SpecialityId,
                        Positive = noteVm.Positive
                    };
                });
            }
            transaction.Commit();


            var decrement = modified && !noteVm.Positive;
            var increment = noteVm.Positive && (modified || !existed);

            if (decrement || increment)
            {
                var isPrioritiezed = await IsPrioritized(noteVm.SpecialityId, noteVm.UserId);
                decrement = decrement && !isPrioritiezed;
                increment = increment && !isPrioritiezed;
            }

            return (id, decrement, increment);
        }

        private async Task<(int id, bool existed)> UpdateExistingHighlightNote(
            SpecialityHighlightNoteVM noteVm)
        {
            int id = -1;
            var existingHighlight = await (
                from note in _notedbContext.Notes
                join highlight in _notedbContext.SpecialityHighlightNotes
                on note.Id equals highlight.NoteId
                where note.UserId == noteVm.UserId
                    && highlight.SpecialityId == noteVm.SpecialityId
                select highlight).Include(n => n.Note).SingleOrDefaultAsync();
            if (existingHighlight is not null && existingHighlight.Positive != noteVm.Positive)
            {
                existingHighlight.SpecialityId = noteVm.SpecialityId;
                existingHighlight.Positive = noteVm.Positive;
                existingHighlight.Note.CreatedAt = DateTime.Now;
                id = existingHighlight.NoteId;
                await _notedbContext.SaveChangesAsync();
            }

            return (id, existingHighlight is not null);
        }

        private async Task<bool> IsPrioritized(int specialityId, int userId)
        {
            var prioritized = await (
                from note in _notedbContext.Notes
                join priority in _notedbContext.SpecialityPriorityNotes
                on note.Id equals priority.NoteId
                where priority.SpecialityId == specialityId && note.UserId == userId
                select priority).FirstOrDefaultAsync();
            return prioritized is not null;
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

        public async Task DeletePlanNoteAsync(int noteId)
        {
            await CascadeDeleteNote(noteId);
        }

        public async Task DeleteSpecialityNoteAsync(int noteId)
        {
            await CascadeDeleteNote(noteId);
        }

        public async Task<bool> DeleteSpecialityHighlightNoteAsync(int noteId)
        {
            var isPrioritized = await IsPrioritized(noteId);
            await CascadeDeleteNote(noteId);
            return isPrioritized;
        }

        public async Task<bool> DeleteSpecialityPriorityNoteAsync(int noteId)
        {
            var isLiked = await IsLiked(noteId);
            await CascadeDeleteNote(noteId);
            return isLiked;
        }

        private async Task CascadeDeleteNote(int id)
        {
            var note = await _notedbContext.Notes
                .SingleAsync(note => note.Id == id);
            _notedbContext.Remove(note);
            await _notedbContext.SaveChangesAsync();
        }

        private async Task<bool> IsPrioritized(int noteId)
        {
            var prioritized = await (
                from note in _notedbContext.Notes
                join priority in _notedbContext.SpecialityPriorityNotes
                on note.Id equals priority.NoteId
                where note.Id == noteId
                select priority).FirstOrDefaultAsync();
            return prioritized is not null;
        }

        private async Task<bool> IsLiked(int noteId)
        {
            var liked = await (
                from note in _notedbContext.Notes
                join highlight in _notedbContext.SpecialityHighlightNotes
                on note.Id equals highlight.NoteId
                where note.Id == noteId && highlight.Positive
                select highlight).SingleOrDefaultAsync();
            return liked is not null;
        }
    }
}
