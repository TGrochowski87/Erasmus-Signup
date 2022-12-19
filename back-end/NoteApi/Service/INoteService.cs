using NoteApi.Models;

namespace NoteApi.Service
{
    public interface INoteService
    {
        Task<IEnumerable<CommonNoteVM>> GetCommonNotesAsync();
        Task<IEnumerable<CommonNoteVM>> GetCommonNotesAsync(int userId);
        Task<IEnumerable<PlanNoteVM>> GetPlanNotesAsync();
        Task<IEnumerable<PlanNoteVM>> GetPlanNotesAsync(int userId);
        Task<IEnumerable<SpecialityNoteVM>> GetSpecialityNotesAsync();
        Task<IEnumerable<SpecialityNoteVM>> GetSpecialityNotesAsync(int userId);
        Task<IEnumerable<SpecialityHighlightNoteVM>> GetSpecialityHighlightNotesAsync();
        Task<IEnumerable<SpecialityHighlightNoteVM>> GetSpecialityHighlightNotesAsync(int userId);
        Task<IEnumerable<SpecialityPriorityNoteVM>> GetSpecialityPriorityNotesAsync();
        Task<IEnumerable<SpecialityPriorityNoteVM>> GetSpecialityPriorityNotesAsync(int userId);
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
        Task<IEnumerable<SpecialityRatingVM>> GetSpecialityRatingNoteAsync(int userId);
    }
}
