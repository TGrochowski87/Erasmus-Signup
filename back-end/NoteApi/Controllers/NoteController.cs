using Microsoft.AspNetCore.Mvc;
using NoteApi.Models;
using NoteApi.Service;
using UserApi.Models;
using UserApi.Service;
using UserApi.Attributes;
using Newtonsoft.Json.Linq;
using UserApi.Controllers;

namespace NoteApi.Controllers
{
    [ApiController]
    [Route("note")]
    public class NoteController : Controller, IUserApiController
    {
        private readonly INoteService noteService;
        private IUserService userService;
        private IAuthorizedService authorizedService;
        public UserJWT? UserToken { get; set; }

        public NoteController(INoteService noteService, IUserService userService,
            IAuthorizedService authorizedService)
        {
            this.noteService = noteService;
            this.userService = userService;
            this.authorizedService = authorizedService;
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
        public async Task<ActionResult<int>> CreatePlanNote(int planId, string content)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.AddPlanNoteAsync(new PlanNoteVM(userId, planId, content)));
        }

        [AuthorizeUser]
        [HttpDelete("plan")]
        public async Task<ActionResult<int>> DeletePlanNote()
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            await noteService.DeletePlanNoteAsync(userId);
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
        public async Task<ActionResult<int>> CreateSpecialityNote(int specialityId, string content)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.AddSpecialityNoteAsync(new SpecialityNoteVM(userId, specialityId, content)));
        }

        [AuthorizeUser]
        [HttpDelete("speciality")]
        public async Task<ActionResult<int>> DeleteSpecialityNote()
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            await noteService.DeleteSpecialityNoteAsync(userId);
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
        public async Task<ActionResult<int>> CreateSpecialityHighlightNote(int specialityId, bool positive)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.AddSpecialityHighlightNoteAsync(
                new SpecialityHighlightNoteVM(userId, specialityId, positive)));
        }

        [AuthorizeUser]
        [HttpDelete("highlight")]
        public async Task<ActionResult> DeletePriorityHighlightNote(
            int noteId, bool positive, int specialityId)
        {
            await noteService.DeleteSpecialityHighlightNoteAsync(noteId, positive, specialityId);
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
        public async Task<ActionResult<int>> CreatePriorityHighlightNote(int specialityId, short priority)
        {
            if (UserToken == null)
            {
                return Unauthorized();
            }

            var userId = GetUserId(UserToken.OAuthAccessToken, UserToken.OAuthAccessTokenSecret);
            return Ok(await noteService.AddSpecialityPriorityNoteAsync(
                new SpecialityPriorityNoteVM(userId, specialityId, priority)));
        }

        [AuthorizeUser]
        [HttpDelete("priority")]
        public async Task<ActionResult> DeletePriorityHighlightNote(int noteId, int specialityId)
        {
            await noteService.DeleteSpecialityPriorityNoteAsync(noteId, specialityId);
            return Ok();
        }
        private int GetUserId(string access_token, string access_token_secret)
        {
            HttpResponseMessage responseMessageUserId = userService.GetCurrentUserId(access_token, access_token_secret);

            if (responseMessageUserId.IsSuccessStatusCode)
            {
                string resultUserId = responseMessageUserId.Content.ReadAsStringAsync().Result;
                JObject jUserId = JObject.Parse(resultUserId);

                if (jUserId.Count > 0)
                {
                    return Convert.ToInt32(jUserId["id"]!.ToString());
                }
            }

            throw new BadHttpRequestException("authorization error: " + responseMessageUserId.ReasonPhrase);
        }
    }
}
