using MassTransit;
using Communication.NoteContracts;

namespace UniversityApi.Consumers
{
    public class NoteCreatedConsumer : IConsumer<NoteCreated>
    {
        public Task Consume(ConsumeContext<NoteCreated> context)
        {
            var message = context.Message;
            return Task.Run(() => { Console.WriteLine($"Got a message {message.Content}."); });
        }
    }
}
