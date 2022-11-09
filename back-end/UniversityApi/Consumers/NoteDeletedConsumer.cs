using MassTransit;
using Communication.NoteContracts;

namespace UniversityApi.Consumers
{
    public class NoteUpdatedConsumer : IConsumer<NoteUpdated>
    {
        public Task Consume(ConsumeContext<NoteUpdated> context)
        {
            var message = context.Message;
            return Task.Run(() => { Console.WriteLine($"Got a message {message.Content}."); });
        }
    }
}
