using NoteApi.DbModels;

namespace NoteApi.Repository
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetListAsync();
        Task<int> AddAsync(string content);
        Task EditAsync(int Id, string content);
        Task DeleteAsync(int Id);
    }
}
