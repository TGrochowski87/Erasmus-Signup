using NoteApi.Models;

namespace NoteApi.Service
{
    public interface INoteService
    {
        Task<IEnumerable<CommonNoteVM>> GetCommonNotesAsync();
        Task<IEnumerable<CommonNoteVM>> GetCommonNotesAsync(long userId);
        Task<IEnumerable<PlanNoteVM>> GetPlanNotesAsync();
        Task<IEnumerable<PlanNoteVM>> GetPlanNotesAsync(long userId);
        Task<IEnumerable<SpecialityNoteVM>> GetSpecialityNotesAsync();
        Task<IEnumerable<SpecialityNoteVM>> GetSpecialityNotesAsync(long userId);
        Task<IEnumerable<SpecialityHighlightNoteVM>> GetSpecialityHighlightNotesAsync();
        Task<IEnumerable<SpecialityHighlightNoteVM>> GetSpecialityHighlightNotesAsync(long userId);
        Task<IEnumerable<SpecialityPriorityNoteVM>> GetSpecialityPriorityNotesAsync();
        Task<IEnumerable<SpecialityPriorityNoteVM>> GetSpecialityPriorityNotesAsync(long userId);
        Task<int> AddCommonNoteAsync(CommonNoteVM note);
        Task<int> AddPlanNoteAsync(PlanNoteVM note);
        Task<int> AddSpecialityNoteAsync(SpecialityNoteVM note);
        Task<int> AddSpecialityHighlightNoteAsync(SpecialityHighlightNoteVM note);
        Task<int> AddSpecialityPriorityNoteAsync(SpecialityPriorityNoteVM note);
        Task UpdateCommonNote(int noteId, CommonNotePostVM noteVm);
        Task UpdatePlanNote(int noteId, PlanNotePostVM noteVm);
        Task UpdateSpecialityNote(int noteId, SpecialityNotePostVM noteVm);
        Task UpdateSpecialityHighlightNote(int noteId, SpecialityHighlightNotePostVM noteVm);
        Task DeleteCommonNoteAsync(int noteId);
        Task DeletePlanNoteAsync(int noteId);
        Task DeleteSpecialityNoteAsync(int noteId);
        Task DeleteSpecialityHighlightNoteAsync(int noteId, bool positive, int specialityId);
        Task DeleteSpecialityPriorityNoteAsync(int noteId, int specialityId);
        Task<IEnumerable<SpecialityRatingVM>> GetSpecialityRatingNoteAsync(long userId);
        Task<IEnumerable<int>> GetFavoriteSpec(long userId);
    }
}
