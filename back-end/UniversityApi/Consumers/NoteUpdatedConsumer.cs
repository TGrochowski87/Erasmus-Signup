using MassTransit;
using Communication.NoteContracts;

namespace UniversityApi.Consumers
{
    public class NoteDeletedConsumer : IConsumer<NoteDeleted>
    {
        public Task Consume(ConsumeContext<NoteDeleted> context)
        {
            var message = context.Message;
            return Task.Run(() => { Console.WriteLine($"Received note deleted with id {message.Id}."); });
        }
    }
}
