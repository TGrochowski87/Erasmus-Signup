using Microsoft.AspNetCore.Mvc;
using UserApi.Models;

namespace UserApi.Controllers

{
    public interface IUserApiController
    {
        public UserJWT? UserToken { get; set; }
    }
}
