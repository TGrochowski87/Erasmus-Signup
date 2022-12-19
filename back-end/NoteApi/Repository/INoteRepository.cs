using NoteApi.Models;
using NoteApi.DbModels;

namespace NoteApi.Repository
{
    public interface INoteRepository
    {
        Task<IEnumerable<CommonNote>> GetCommonNotesAsync();
        Task<IEnumerable<CommonNote>> GetCommonNotesAsync(int userId);
        Task<IEnumerable<PlanNote>> GetPlanNotesAsync();
        Task<IEnumerable<PlanNote>> GetPlanNotesAsync(int userId);
        Task<IEnumerable<SpecialityNote>> GetSpecialityNotesAsync();
        Task<IEnumerable<SpecialityNote>> GetSpecialityNotesAsync(int userId);
        Task<IEnumerable<SpecialityHighlightNote>> GetSpecialityHighlightNotesAsync();
        Task<IEnumerable<SpecialityHighlightNote>> GetSpecialityHighlightNotesAsync(int userId);
        Task<IEnumerable<SpecialityPriorityNote>> GetSpecialityPriorityNotesAsync();
        Task<IEnumerable<SpecialityPriorityNote>> GetSpecialityPriorityNotesAsync(int userId);
        Task<int> AddCommonNoteAsync(CommonNoteVM noteVm);
        Task<int> AddPlanNoteAsync(PlanNoteVM noteVm);
        Task<int> AddSpecialityNoteAsync(SpecialityNoteVM noteVm);
        Task<(int id, bool decrement, bool increment)> AddSpecialityHighlightNoteAsync(
            SpecialityHighlightNoteVM noteVm);
        Task<(int id, int decrement, int increment)> AddSpecialityPriorityNoteAsync(
            SpecialityPriorityNoteVM noteVm);
        Task UpdateCommonNote(int noteId, CommonNotePostVM noteVm);
        Task UpdatePlanNote(int noteId, PlanNotePostVM noteVm);
        Task UpdateSpecialityNote(int noteId, SpecialityNotePostVM noteVm);
        Task<bool> UpdateSpecialityHighlightNote(int noteId, SpecialityHighlightNotePostVM noteVm);
        Task DeleteCommonNoteAsync(int noteId);
        Task DeletePlanNoteAsync(int noteId);
        Task DeleteSpecialityNoteAsync(int noteId);
        Task<bool> DeleteSpecialityHighlightNoteAsync(int noteId);
        Task<bool> DeleteSpecialityPriorityNoteAsync(int noteId);
    }
}
