using UniversityApi.Models;

namespace UniversityApi.Service
{
    public class UniversityService : IUniversityService
    {
        public ExampleModel Example()
        {
            return new ExampleModel("Example");
        }
    }
}
