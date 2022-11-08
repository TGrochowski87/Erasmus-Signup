using NoteApi.DbModels;

namespace NoteApi.Repository
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetList();
        Task Add(Note note);
    }
}
