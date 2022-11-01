using NoteApi.Models;

namespace NoteApi.Service
{
    public class NoteService : INoteService
    {
        public ExampleModel Example()
        {
            return new ExampleModel("Example");
        }
    }
}
