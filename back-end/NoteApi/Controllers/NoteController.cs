using Microsoft.AspNetCore.Mvc;
using NoteApi.Models;
using NoteApi.Service;
using NoteApi.Attributes;

namespace NoteApi.Controllers
{
    [ApiController]
    [Route("note")]
    public class NoteController : Controller, INoteController
    {
        private readonly INoteService noteService;
        public UserJWT? UserToken { get; set; }

        public NoteController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        [AuthorizeUser]
        [HttpGet("common")]
        public async Task<ActionResult<IEnumerable<CommonNoteVM>>> GetCommon()
        {
            var userId = GetUserId();
            return Ok(await noteService.GetCommonNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("common")]
        public async Task<ActionResult<int>> CreateCommonNote([FromBody] CommonNotePostVM note)
        {
            var userId = GetUserId();
            return Ok(await noteService.AddCommonNoteAsync(new CommonNoteVM(userId, note)));
        }

        [AuthorizeUser]
        [HttpPut("common/{noteId}")]
        public async Task<ActionResult> UpdateCommonNote(int noteId, [FromBody] CommonNotePostVM note)
        {
            await noteService.UpdateCommonNote(noteId, note);
            return Ok();
        }

        [AuthorizeUser]
        [HttpDelete("common/{noteId}")]
        public async Task<ActionResult> DeleteCommonNote(int noteId)
        {
            await noteService.DeleteCommonNoteAsync(noteId);
            return Ok();
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

        [AuthorizeUser]
        [HttpGet("plan")]
        public async Task<ActionResult<IEnumerable<PlanNoteVM>>> GetPlan()
        {
            var userId = GetUserId();
            return Ok(await noteService.GetPlanNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("plan")]
        public async Task<ActionResult<int>> CreatePlanNote([FromBody] PlanNotePostVM note)
        {
            var userId = GetUserId();
            return Ok(await noteService.AddPlanNoteAsync(new PlanNoteVM(userId, note)));
        }

        [AuthorizeUser]
        [HttpPut("plan/{noteId}")]
        public async Task<ActionResult> UpdatePlanNote(int noteId, [FromBody] PlanNotePostVM note)
        {
            await noteService.UpdatePlanNote(noteId, note);
            return Ok();
        }

        [AuthorizeUser]
        [HttpDelete("plan/{noteId}")]
        public async Task<ActionResult> DeletePlanNote(int noteId)
        {
            await noteService.DeletePlanNoteAsync(noteId);
            return Ok();
        }

        /*
         * jak wyzej
         * 
        [HttpGet("specialities")]
        public async Task<Result<IEnumerable<SpecialityNoteVM>>> GetSpeciality()
        {
            return Result.Ok(await noteService.GetSpecialityNotesAsync());
        }
        */

        [AuthorizeUser]
        [HttpGet("speciality")]
        public async Task<ActionResult<IEnumerable<SpecialityNoteVM>>> GetSpeciality()
        {
            var userId = GetUserId();
            return Ok(await noteService.GetSpecialityNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("speciality")]
        public async Task<ActionResult<int>> CreateSpecialityNote([FromBody] SpecialityNotePostVM note)
        {
            var userId = GetUserId();
            return Ok(await noteService.AddSpecialityNoteAsync(new SpecialityNoteVM(userId, note)));
        }

        [AuthorizeUser]
        [HttpPut("speciality/{noteId}")]
        public async Task<ActionResult> UpdateSpecialityNote(int noteId, [FromBody] SpecialityNotePostVM note)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            await noteService.UpdateSpecialityNote(noteId, note);
            return Ok();
        }

        [AuthorizeUser]
        [HttpDelete("speciality/{noteId}")]
        public async Task<ActionResult> DeleteSpecialityNote(int noteId)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            await noteService.DeleteSpecialityNoteAsync(noteId);
            return Ok();
        }

        /*
        [HttpGet("highlights")]
        public async Task<Result<IEnumerable<SpecialityHighlightNoteVM>>> GetHighlight()
        {
            return Result.Ok(await noteService.GetSpecialityHighlightNotesAsync());
        }
        */

        [AuthorizeUser]
        [HttpGet("highlight")]
        public async Task<ActionResult<IEnumerable<SpecialityHighlightNoteVM>>> GetHighlight()
        {
            var userId = GetUserId();
            return Ok(await noteService.GetSpecialityHighlightNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("highlight")]
        public async Task<ActionResult<int>> CreateSpecialityHighlightNote([FromBody] SpecialityHighlightNotePostVM note)
        {
            var userId = GetUserId();
            return Ok(await noteService.AddSpecialityHighlightNoteAsync(new SpecialityHighlightNoteVM(userId, note)));
        }

        [AuthorizeUser]
        [HttpPut("highlight/{noteId}")]
        public async Task<ActionResult> UpdateSpecialityHighlightNote(int noteId, [FromBody] SpecialityHighlightNotePostVM note)
        {
            await noteService.UpdateSpecialityHighlightNote(noteId, note);
            return Ok();
        }

        [AuthorizeUser]
        [HttpDelete("highlight/{noteId}")]
        public async Task<ActionResult> DeleteSpecialityHighlightNote(int noteId, [FromBody] SpecialityHighlightNotePostVM note)
        {
            await noteService.DeleteSpecialityHighlightNoteAsync(noteId, note.Positive, note.SpecialityId);
            return Ok();
        }

        /*
        [HttpGet("priorities")]
        public async Task<Result<IEnumerable<SpecialityPriorityNoteVM>>> GetPriority()
        {
            return Result.Ok(await noteService.GetSpecialityPriorityNotesAsync());
        }
        */

        [AuthorizeUser]
        [HttpGet("priority")]
        public async Task<ActionResult<IEnumerable<SpecialityPriorityNoteVM>>> GetPriority()
        {
            var userId = GetUserId();
            return Ok(await noteService.GetSpecialityPriorityNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("priority")]
        public async Task<ActionResult<int>> CreateSpecialityPriorityNote([FromBody] SpecialityPriorityNotePostVM note)
        {
            var userId = GetUserId();
            return Ok(await noteService.AddSpecialityPriorityNoteAsync(new SpecialityPriorityNoteVM(userId, note)));
        }

        [AuthorizeUser]
        [HttpDelete("priority/{noteId}")]
        public async Task<ActionResult> DeletePriorityHighlightNote(int noteId, [FromBody] int specialityId)
        {
            await noteService.DeleteSpecialityPriorityNoteAsync(noteId, specialityId);
            return Ok();
        }

        [AuthorizeUser]
        [HttpGet("user_rating")]
        public async Task<ActionResult<IEnumerable<SpecialityRatingVM>>> GetSpecialityRatingNotes()
        {
            var userId = GetUserId();
            return Ok(await noteService.GetSpecialityRatingNoteAsync(userId));
        }

        [AuthorizeUser]
        [HttpGet("favorite-spec/{userId}")]
        public async Task<IEnumerable<int>> GetFavoriteSpec([FromRoute] long userId)
        {
            return await noteService.GetFavoriteSpec(userId);
        }

        private long GetUserId()
        {
            if (UserToken == null)
            { 
                throw new BadHttpRequestException("authorization error: UserToken is null");
            }

            return long.Parse(UserToken.UserId);
        }
    }
}
