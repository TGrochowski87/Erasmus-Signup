using FluentResults;
using Microsoft.AspNetCore.Mvc;
using NoteApi.DbModels;
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

        // example
        [HttpGet]
        public async Task<Result<IEnumerable<NoteVM>>> GetList()
        {
            var notes = await noteService.GetList();
            return Result.Ok(notes);
        }

        // example
        [HttpPost("add")]
        public async Task<Result> AddNote(NoteVM note)
        {
            await noteService.AddNote(note);
            return Result.Ok();
        }

        // example
        [HttpPost("edit")]
        public async Task<Result> EditNote(NoteVM note)
        {
            await noteService.EditNote(note);
            return Result.Ok();
        }

        // example
        [HttpDelete("{id}")]
        public async Task<Result> DeleteNote(int id)
        {
            await noteService.DeleteNote(id);
            return Result.Ok();
        }
    }
}
