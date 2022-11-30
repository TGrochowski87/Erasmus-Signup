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
            return (await _noteRepository.GetListAsync()).Select(x => new NoteVM(x));
        }

        public async Task AddNote(NoteVM noteVM)
        {
            int Id = await _noteRepository.AddAsync(noteVM.Content);
            await _publishEndpoint.Publish(new NoteCreated(Id, noteVM.Content));
        }

        public async Task EditNote(NoteVM noteVM)
        {
            await _noteRepository.EditAsync(noteVM.Id, noteVM.Content);
            await _publishEndpoint.Publish(new NoteUpdated(noteVM.Id, noteVM.Content));
        }

        public async Task DeleteNote(int Id)
        {
            await _noteRepository.DeleteAsync(Id);
            await _publishEndpoint.Publish(new NoteDeleted(Id));
        }
    }
}
