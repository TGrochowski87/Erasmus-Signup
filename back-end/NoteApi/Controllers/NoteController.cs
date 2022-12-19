using Microsoft.AspNetCore.Mvc;
using NoteApi.Models;
using NoteApi.Service;
using UserApi.Models;
using UserApi.Service;
using UserApi.Attributes;
using Newtonsoft.Json.Linq;
using UserApi.Controllers;
using NoteApi.DbModels;

namespace NoteApi.Controllers
{
    [ApiController]
    [Route("note")]
    public class NoteController : Controller, IUserApiController
    {
        private readonly INoteService noteService;
        private readonly IUserService userService;
        public UserJWT? UserToken { get; set; }

        public NoteController(INoteService noteService, IUserService userService)
        {
            this.noteService = noteService;
            this.userService = userService;
        }

        [AuthorizeUser]
        [HttpGet("common")]
        public async Task<ActionResult<IEnumerable<CommonNoteVM>>> GetCommon()
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.GetCommonNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("common")]
        public async Task<ActionResult<int>> CreateCommonNote([FromBody] CommonNotePostVM note)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.AddCommonNoteAsync(new CommonNoteVM(userId, note)));
        }

        [AuthorizeUser]
        [HttpPut("common/{noteId}")]
        public async Task<ActionResult> UpdateCommonNote(int noteId, [FromBody] CommonNotePostVM note)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            await noteService.UpdateCommonNote(noteId, note);
            return Ok();
        }

        [AuthorizeUser]
        [HttpDelete("common/{noteId}")]
        public async Task<ActionResult> DeleteCommonNote(int noteId)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

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
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.GetPlanNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("plan")]
        public async Task<ActionResult<int>> CreatePlanNote([FromBody] PlanNotePostVM note)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.AddPlanNoteAsync(new PlanNoteVM(userId, note)));
        }

        [AuthorizeUser]
        [HttpPut("plan/{noteId}")]
        public async Task<ActionResult> UpdatePlanNote(int noteId, [FromBody] PlanNotePostVM note)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            await noteService.UpdatePlanNote(noteId, note);
            return Ok();
        }

        [AuthorizeUser]
        [HttpDelete("plan/{noteId}")]
        public async Task<ActionResult> DeletePlanNote(int noteId)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

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
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.GetSpecialityNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("speciality")]
        public async Task<ActionResult<int>> CreateSpecialityNote([FromBody] SpecialityNotePostVM note)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
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
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.GetSpecialityHighlightNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("highlight")]
        public async Task<ActionResult<int>> CreateSpecialityHighlightNote([FromBody] SpecialityHighlightNotePostVM note)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.AddSpecialityHighlightNoteAsync(new SpecialityHighlightNoteVM(userId, note)));
        }

        [AuthorizeUser]
        [HttpPut("highlight/{noteId}")]
        public async Task<ActionResult> UpdateSpecialityHighlightNote(int noteId, [FromBody] SpecialityHighlightNotePostVM note)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

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
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.GetSpecialityPriorityNotesAsync(userId));
        }

        [AuthorizeUser]
        [HttpPost("priority")]
        public async Task<ActionResult<int>> CreateSpecialityPriorityNote([FromBody] SpecialityPriorityNotePostVM note)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.AddSpecialityPriorityNoteAsync(new SpecialityPriorityNoteVM(userId, note)));
        }

        [AuthorizeUser]
        [HttpDelete("priority/{noteId}")]
        public async Task<ActionResult> DeletePriorityHighlightNote(int noteId, [FromBody] int specialityId)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            await noteService.DeleteSpecialityPriorityNoteAsync(noteId, specialityId);
            return Ok();
        }

        [AuthorizeUser]
        [HttpGet("user_rating")]
        public async Task<ActionResult<IEnumerable<SpecialityRatingVM>>> GetSpecialityRatingNotes()
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.GetSpecialityRatingNoteAsync(userId));
        }

        private long GetUserId(string access_token, string access_token_secret)
        {
            HttpResponseMessage responseMessageUserId = userService.GetCurrentUserId(access_token, access_token_secret);

            if (responseMessageUserId.IsSuccessStatusCode)
            {
                string resultUserId = responseMessageUserId.Content.ReadAsStringAsync().Result;
                JObject jUserId = JObject.Parse(resultUserId);

                if (jUserId.Count > 0)
                {
                    return Convert.ToInt64(jUserId["id"]!.ToString());
                }
            }

            throw new BadHttpRequestException("authorization error: " + responseMessageUserId.ReasonPhrase);
        }
    }
}
