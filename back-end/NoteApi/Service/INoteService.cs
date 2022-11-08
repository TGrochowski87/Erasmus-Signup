using NoteApi.Models;

namespace NoteApi.Service
{
    public interface INoteService
    {
        ExampleModel Example();
        public Task<IEnumerable<NoteVM>> GetList();
        public Task AddNote(NoteVM noteVM);
    }
}
