using FluentResults;
using Microsoft.AspNetCore.Mvc;
using NoteApi.Models;
using NoteApi.Service;

namespace NoteApi.Controllers
{
    [ApiController]
    [Route("note")]
    public class NoteController : Controller
    {
        private INoteService noteService;

        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        [HttpGet("example")]
        public Result<ExampleModel> Example()
        {
            return Result.Ok(noteService.Example());
        }
    }
}
