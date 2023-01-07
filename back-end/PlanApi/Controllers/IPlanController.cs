using PlanApi.Models;

namespace PlanApi.Controllers
{
    public interface IPlanController
    {
        UserJWT? UserToken { get; set; }
    }
}
