using OpinionApi.Models;

namespace OpinionApi.Service
{
    public class OpinionService : IOpinionService
    {
        public ExampleModel Example()
        {
            return new ExampleModel("Example");
        }
    }
}
