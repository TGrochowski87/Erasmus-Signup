using NoteApi.Models;
using NoteApi.DbModels;

namespace NoteApi.Repository
{
    public interface INoteRepository
    {
        Task<IEnumerable<PlanNote>> GetPlanNotesAsync();
        Task<IEnumerable<PlanNote>> GetPlanNotesAsync(int userId);
        Task<IEnumerable<SpecialityNote>> GetSpecialityNotesAsync();
        Task<IEnumerable<SpecialityNote>> GetSpecialityNotesAsync(int userId);
        Task<IEnumerable<SpecialityHighlightNote>> GetSpecialityHighlightNotesAsync();
        Task<IEnumerable<SpecialityHighlightNote>> GetSpecialityHighlightNotesAsync(int userId);
        Task<IEnumerable<SpecialityPriorityNote>> GetSpecialityPriorityNotesAsync();
        Task<IEnumerable<SpecialityPriorityNote>> GetSpecialityPriorityNotesAsync(int userId);
        Task<int> AddPlanNoteAsync(PlanNoteVM note);
        Task<int> AddSpecialityNoteAsync(SpecialityNoteVM note);
        Task<int> AddSpecialityHighlightNoteAsync(SpecialityHighlightNoteVM note);
        Task<int> AddSpecialityPriorityNoteAsync(SpecialityPriorityNoteVM note);
    }
}
