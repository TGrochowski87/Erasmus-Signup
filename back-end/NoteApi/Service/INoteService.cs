using NoteApi.Models;

namespace NoteApi.Service
{
    public interface INoteService
    {
        Task<IEnumerable<PlanNoteVM>> GetPlanNotesAsync();
        Task<IEnumerable<PlanNoteVM>> GetPlanNotesAsync(int userId);
        Task<IEnumerable<SpecialityNoteVM>> GetSpecialityNotesAsync();
        Task<IEnumerable<SpecialityNoteVM>> GetSpecialityNotesAsync(int userId);
        Task<IEnumerable<SpecialityHighlightNoteVM>> GetSpecialityHighlightNotesAsync();
        Task<IEnumerable<SpecialityHighlightNoteVM>> GetSpecialityHighlightNotesAsync(int userId);
        Task<IEnumerable<SpecialityPriorityNoteVM>> GetSpecialityPriorityNotesAsync();
        Task<IEnumerable<SpecialityPriorityNoteVM>> GetSpecialityPriorityNotesAsync(int userId);
        Task<int> AddSpecialityNoteAsync(SpecialityNoteVM note);
        Task<int> AddSpecialityHighlightNoteAsync(SpecialityHighlightNoteVM note);
        Task<int> AddSpecialityPriorityNoteAsync(SpecialityPriorityNoteVM note);
    }
}
