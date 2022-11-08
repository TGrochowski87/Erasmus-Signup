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

        [HttpGet]
        public async Task<Result<IEnumerable<NoteVM>>> GetList()
        {
            var notes = await noteService.GetList();
            return Result.Ok(notes);
        }

        [HttpPost]
        public async Task<Result> AddNote(NoteVM note)
        {
            await noteService.AddNote(note);
            return Result.Ok();
        }
    }
}
