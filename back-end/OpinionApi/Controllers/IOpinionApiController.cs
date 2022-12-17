using OpinionApi.Models;

namespace OpinionApi.Controllers
{
    public interface IOpinionApiController
    {
        public UserJWT? UserToken { get; set; }
    }
}
