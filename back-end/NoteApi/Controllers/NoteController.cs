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

        [HttpGet("plan")]
        public async Task<Result<IEnumerable<PlanNoteVM>>> GetPlan()
        {
            return Result.Ok(await noteService.GetPlanNotesAsync());
        }
        
        [HttpGet("plan/{userId}")]
        public async Task<Result<IEnumerable<PlanNoteVM>>> GetPlan(int userId)
        {
            return Result.Ok(await noteService.GetPlanNotesAsync(userId));
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
    }
}
