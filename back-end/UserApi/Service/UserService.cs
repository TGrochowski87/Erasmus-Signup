using UserApi.Models;

namespace UserApi.Service
{
    public class UserService : IUserService
    {
        public ExampleModel Example()
        {
            return new ExampleModel("Example");
        }
    }
}
