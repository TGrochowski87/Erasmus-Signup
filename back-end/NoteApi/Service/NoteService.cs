using MassTransit;
using Communication.NoteContracts;
using NoteApi.Models;
using NoteApi.Repository;

namespace NoteApi.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public NoteService(INoteRepository noteRepository, IPublishEndpoint publishEndpoint)
        {
            _noteRepository = noteRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<IEnumerable<CommonNoteVM>> GetCommonNotesAsync()
        {
            var list = await _noteRepository.GetCommonNotesAsync();
            return list.Select(x => new CommonNoteVM(x));
        }

        public async Task<IEnumerable<CommonNoteVM>> GetCommonNotesAsync(int userId)
        {
            var list = await _noteRepository.GetCommonNotesAsync(userId);
            return list.Select(x => new CommonNoteVM(x));
        }

        public async Task<IEnumerable<PlanNoteVM>> GetPlanNotesAsync()
        {
            var list = await _noteRepository.GetPlanNotesAsync();
            return list.Select(x => new PlanNoteVM(x));
        }

        public async Task<IEnumerable<PlanNoteVM>> GetPlanNotesAsync(int userId)
        {
            var list = await _noteRepository.GetPlanNotesAsync(userId);
            return list.Select(x => new PlanNoteVM(x));
        }

        public async Task<IEnumerable<SpecialityNoteVM>> GetSpecialityNotesAsync()
        {
            var list = await _noteRepository.GetSpecialityNotesAsync();
            return list.Select(x => new SpecialityNoteVM(x));
        }

        public async Task<IEnumerable<SpecialityNoteVM>> GetSpecialityNotesAsync(int userId)
        {
            var list = await _noteRepository.GetSpecialityNotesAsync(userId);
            return list.Select(x => new SpecialityNoteVM(x));
        }

        public async Task<IEnumerable<SpecialityHighlightNoteVM>> GetSpecialityHighlightNotesAsync()
        {
            var list = await _noteRepository.GetSpecialityHighlightNotesAsync();
            return list.Select(x => new SpecialityHighlightNoteVM(x));
        }

        public async Task<IEnumerable<SpecialityHighlightNoteVM>> GetSpecialityHighlightNotesAsync(int userId)
        {
            var list = await _noteRepository.GetSpecialityHighlightNotesAsync(userId);
            return list.Select(x => new SpecialityHighlightNoteVM(x));
        }

        public async Task<IEnumerable<SpecialityPriorityNoteVM>> GetSpecialityPriorityNotesAsync()
        {
            var list = await _noteRepository.GetSpecialityPriorityNotesAsync();
            return list.Select(x => new SpecialityPriorityNoteVM(x));
        }

        public async Task<IEnumerable<SpecialityPriorityNoteVM>> GetSpecialityPriorityNotesAsync(int userId)
        {
            var list = await _noteRepository.GetSpecialityPriorityNotesAsync(userId);
            return list.Select(x => new SpecialityPriorityNoteVM(x));
        }

        public async Task<int> AddCommonNoteAsync(CommonNoteVM note)
        {
            return await _noteRepository.AddCommonNoteAsync(note);
        }

        public async Task<int> AddPlanNoteAsync(PlanNoteVM note)
        {
            return await _noteRepository.AddPlanNoteAsync(note);
        }

        public async Task<int> AddSpecialityHighlightNoteAsync(SpecialityHighlightNoteVM note)
        {
            var (id, decrement, increment) = await _noteRepository.AddSpecialityHighlightNoteAsync(note);
            if (decrement || increment)
            {
                await _publishEndpoint.Publish(
                    new SpecialityInterestNote(note.SpecialityId, increment, 0));
            }

            return id;
        }

        public async Task<int> AddSpecialityNoteAsync(SpecialityNoteVM note)
        {
            return await _noteRepository.AddSpecialityNoteAsync(note);
        }

        public async Task<int> AddSpecialityPriorityNoteAsync(SpecialityPriorityNoteVM note)
        {
            var (id, decrement, increment) = await _noteRepository
                .AddSpecialityPriorityNoteAsync(note);
            if (decrement >= 0)
            {
                await _publishEndpoint.Publish(
                    new SpecialityInterestNote(decrement, false, 0));
            }
            if (increment >= 0)
            {
                await _publishEndpoint.Publish(
                    new SpecialityInterestNote(increment, true, 0));
            }

            return id;
        }

        public async Task UpdateCommonNote(int noteId, CommonNotePostVM noteVm)
        {
            await _noteRepository.UpdateCommonNote(noteId, noteVm);
        }

        public async Task UpdatePlanNote(int noteId, PlanNotePostVM noteVm)
        {
            await _noteRepository.UpdatePlanNote(noteId, noteVm);
        }

        public async Task UpdateSpecialityNote(int noteId, SpecialityNotePostVM noteVm)
        {
            await _noteRepository.UpdateSpecialityNote(noteId, noteVm);
        }

        public async Task UpdateSpecialityHighlightNote(int noteId, SpecialityHighlightNotePostVM noteVm)
        {
            bool modify = await _noteRepository.UpdateSpecialityHighlightNote(noteId, noteVm);
            if (modify)
            {
                await _publishEndpoint.Publish(
                    new SpecialityInterestNote(noteVm.SpecialityId, noteVm.Positive, 0));
            }
        }

        public async Task DeleteCommonNoteAsync(int noteId)
        {
            await _noteRepository.DeleteCommonNoteAsync(noteId);
        }

        public async Task DeletePlanNoteAsync(int noteId)
        {
            await _noteRepository.DeletePlanNoteAsync(noteId);
        }

        public async Task DeleteSpecialityNoteAsync(int noteId)
        {
            await _noteRepository.DeleteSpecialityNoteAsync(noteId);
        }

        public async Task<IEnumerable<SpecialityRatingVM>> GetSpecialityRatingNoteAsync(int userId)
        {
            var highlights = await _noteRepository.GetSpecialityHighlightNotesAsync();
            var priorities = await _noteRepository.GetSpecialityPriorityNotesAsync();
            var hRatings = highlights.Select(x => new SpecialityRatingVM(x));
            var pRatings = priorities.Select(x => new SpecialityRatingVM(x));
            return hRatings.Concat(pRatings);
        }

        public async Task DeleteSpecialityHighlightNoteAsync(
            int noteId, bool positive, int specialityId)
        {
            var isPrioritized = await _noteRepository.DeleteSpecialityHighlightNoteAsync(noteId);
            if (positive && !isPrioritized)
            {
                await _publishEndpoint.Publish(
                    new SpecialityInterestNote(specialityId, false, 0));
            }
        }

        public async Task DeleteSpecialityPriorityNoteAsync(int noteId, int specialityId)
        {
            var isLiked = await _noteRepository.DeleteSpecialityPriorityNoteAsync(noteId);
            if (!isLiked)
            {
                await _publishEndpoint.Publish(
                    new SpecialityInterestNote(specialityId, false, 0));
            }
        }
    }
}
