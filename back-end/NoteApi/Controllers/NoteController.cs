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
        private readonly INoteService noteService;

        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        /*
         * na razie koment bo moze nie bedzie use case'a na to
         * 
        [HttpGet("plans")]
        public async Task<Result<IEnumerable<PlanNoteVM>>> GetPlan()
        {
            return Result.Ok(await noteService.GetPlanNotesAsync());
        }
        */

        [HttpGet("plan")]
        public async Task<Result<IEnumerable<PlanNoteVM>>> GetPlan(string acces_token, string acces_token_secret)
        {
            return Result.Ok(await noteService.GetPlanNotesAsync(userId));
        }

        [HttpPost("plan")]
        public async Task<Result<int>> CreatePlanNote(PlanNoteVM note)
        {
            return Result.Ok(await noteService.AddPlanNoteAsync(note));
        }

        [HttpDelete("plan")]
        public async Task<Result<int>> DeletePlanNote(int noteId)
        {
            await noteService.DeletePlanNoteAsync(noteId);
            return Result.Ok();
        }

        [HttpGet("speciality")]
        public async Task<Result<IEnumerable<SpecialityNoteVM>>> GetSpeciality()
        {
            return Result.Ok(await noteService.GetSpecialityNotesAsync());
        }

        [HttpGet("speciality/{userId}")]
        public async Task<Result<IEnumerable<SpecialityNoteVM>>> GetSpeciality(int userId)
        {
            return Result.Ok(await noteService.GetSpecialityNotesAsync(userId));
        }

        [HttpPost("speciality")]
        public async Task<Result<int>> CreateSpecialityNote(SpecialityNoteVM note)
        {
            return Result.Ok(await noteService.AddSpecialityNoteAsync(note));
        }

        [HttpDelete("speciality")]
        public async Task<Result<int>> DeleteSpecialityNote(int noteId)
        {
            await noteService.DeleteSpecialityNoteAsync(noteId);
            return Result.Ok();
        }

        [HttpGet("highlight")]
        public async Task<Result<IEnumerable<SpecialityHighlightNoteVM>>> GetHighlight()
        {
            return Result.Ok(await noteService.GetSpecialityHighlightNotesAsync());
        }

        [HttpGet("highlight/{userId}")]
        public async Task<Result<IEnumerable<SpecialityHighlightNoteVM>>> GetHighlight(int userId)
        {
            return Result.Ok(await noteService.GetSpecialityHighlightNotesAsync(userId));
        }

        [HttpPost("highlight")]
        public async Task<Result<int>> CreateSpecialityHighlightNote(SpecialityHighlightNoteVM note)
        {
            return Result.Ok(await noteService.AddSpecialityHighlightNoteAsync(note));
        }

        [HttpDelete("highlight")]
        public async Task<Result> DeletePriorityHighlightNote(
            int noteId, bool positive, int specialityId)
        {
            await noteService.DeleteSpecialityHighlightNoteAsync(noteId, positive, specialityId);
            return Result.Ok();
        }

        [HttpGet("priority")]
        public async Task<Result<IEnumerable<SpecialityPriorityNoteVM>>> GetPriority()
        {
            return Result.Ok(await noteService.GetSpecialityPriorityNotesAsync());
        }

        [HttpGet("priority/{userId}")]
        public async Task<Result<IEnumerable<SpecialityPriorityNoteVM>>> GetPriority(int userId)
        {
            return Result.Ok(await noteService.GetSpecialityPriorityNotesAsync(userId));
        }

        [HttpPost("priority")]
        public async Task<Result<int>> CreatePriorityHighlightNote(SpecialityPriorityNoteVM note)
        {
            return Result.Ok(await noteService.AddSpecialityPriorityNoteAsync(note));
        }

        [HttpDelete("priority")]
        public async Task<Result> DeletePriorityHighlightNote(int noteId, int specialityId)
        {
            await noteService.DeleteSpecialityPriorityNoteAsync(noteId, specialityId);
            return Result.Ok();
        }
    }
}
