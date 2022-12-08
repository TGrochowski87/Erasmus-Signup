using Communication.NoteContracts;
using MassTransit;
using UniversityApi.Repository;

namespace UniversityApi.Consumers
{
    public class SpecialityInterestNoteConsumer : IConsumer<SpecialityInterestNote>
    {
        private readonly IUniversityRepository _universityRepository;

        public SpecialityInterestNoteConsumer(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }

        public async Task Consume(ConsumeContext<SpecialityInterestNote> context)
        {
            var message = context.Message;
            await _universityRepository
                .UpdateInterestedStudentsCountAsync(message.SpecialityId, message.Positive);
        }
    }
}
