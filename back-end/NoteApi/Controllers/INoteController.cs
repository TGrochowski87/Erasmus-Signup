using NoteApi.Models;

namespace NoteApi.Controllers
{
    public interface INoteController
    {
        public UserJWT? UserToken { get; set; }
    }
}
