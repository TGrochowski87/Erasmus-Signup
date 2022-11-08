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

        public ExampleModel Example()
        {
            return new ExampleModel("Example");
        }

        public async Task<IEnumerable<NoteVM>> GetList()
        {
            return (await _noteRepository.GetList()).Select(x => new NoteVM(x));
        }

        public async Task AddNote(NoteVM noteVM)
        {
            await _noteRepository.Add(new DbModels.Note
            {
                Id = noteVM.Id,
                Content = noteVM.Content
            });
            await _publishEndpoint.Publish(new NoteCreated(noteVM.Id, noteVM.Content));
        }
    }
}
