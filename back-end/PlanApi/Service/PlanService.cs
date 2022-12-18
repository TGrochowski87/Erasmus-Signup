using PlanApi.Models;

namespace PlanApi.Service
{
    public class PlanService : IPlanService
    {
        public ExampleModel Example()
        {
            return new ExampleModel("Example");
        }
    }
}
