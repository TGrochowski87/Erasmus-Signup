using ErasmusRabbitContracts.UniversityContracts;
using MassTransit;
using UserApi.Models;
using UserApi.Service;

namespace UserApi.Consumers
{
    public class ProfileGetUniversityConsumer : IConsumer<ProfileGet>
    {
        private readonly IUserService _userService;

        public ProfileGetUniversityConsumer(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<ProfileGet> context)
        {
            var message = context.Message;
            var student = await _userService
                .GetStudent(message.UserId);

            await context.RespondAsync<ProfileGetResult>(
                new 
                { 
                    student.PreferencedStudyDomainId,
                    student.AverageGrade 
                });
        }
    }
}
