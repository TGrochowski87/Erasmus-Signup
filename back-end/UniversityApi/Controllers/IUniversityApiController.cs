using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    public interface IUniversityApiController
    {
        public UserJWT? UserToken { get; set; }
    }
}
