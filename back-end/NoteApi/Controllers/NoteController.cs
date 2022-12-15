using FluentResults;
using Microsoft.AspNetCore.Mvc;
using NoteApi.Models;
using NoteApi.Service;
using System.Collections.Specialized;
using UserApi.Models;
using UserApi.Service;
using UserApi.Utilities;
using UserApi.Attributes;
using Newtonsoft.Json.Linq;


namespace NoteApi.Controllers
{
    [ApiController]
    [Route("note")]
    public class NoteController : Controller
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

        [HttpGet("plan")]
        public async Task<Result<IEnumerable<PlanNoteVM>>> GetPlan(string access_token, string access_token_secret)
        {
            var userId = GetUserId(access_token, access_token_secret);
            return Result.Ok(await noteService.GetPlanNotesAsync(Convert.ToInt32(userId)));
        }

        [HttpPost("plan")]
        public async Task<Result<int>> CreatePlanNote(PlanNoteVM note)
        {
            return Result.Ok(await noteService.AddPlanNoteAsync(note));
        }

        [HttpDelete("plan")]
        public async Task<Result<int>> DeletePlanNote(string access_token, string access_token_secret)
        {
            var userId = GetUserId(access_token, access_token_secret);
            await noteService.DeletePlanNoteAsync(Convert.ToInt32(userId));
            return Result.Ok();
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

        [HttpGet("speciality")]
        public async Task<Result<IEnumerable<SpecialityNoteVM>>> GetSpeciality(string access_token, string access_token_secret)
        {
            var userId = GetUserId(access_token, access_token_secret);
            return Result.Ok(await noteService.GetSpecialityNotesAsync(Convert.ToInt32(userId)));
        }

        [HttpPost("speciality")]
        public async Task<Result<int>> CreateSpecialityNote(SpecialityNoteVM note)
        {
            return Result.Ok(await noteService.AddSpecialityNoteAsync(note));
        }

        [HttpDelete("speciality")]
        public async Task<Result<int>> DeleteSpecialityNote(string access_token, string access_token_secret)
        {
            var userId = GetUserId(access_token, access_token_secret);
            await noteService.DeleteSpecialityNoteAsync(Convert.ToInt32(userId));
            return Result.Ok();
        }

        /*
        [HttpGet("highlights")]
        public async Task<Result<IEnumerable<SpecialityHighlightNoteVM>>> GetHighlight()
        {
            return Result.Ok(await noteService.GetSpecialityHighlightNotesAsync());
        }
        */

        [HttpGet("highlight")]
        public async Task<Result<IEnumerable<SpecialityHighlightNoteVM>>> GetHighlight(string access_token, string access_token_secret)
        {
            var userId = GetUserId(access_token, access_token_secret);
            return Result.Ok(await noteService.GetSpecialityHighlightNotesAsync(Convert.ToInt32(userId)));
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

        /*
        [HttpGet("priorities")]
        public async Task<Result<IEnumerable<SpecialityPriorityNoteVM>>> GetPriority()
        {
            return Result.Ok(await noteService.GetSpecialityPriorityNotesAsync());
        }
        */

        [HttpGet("priority")]
        public async Task<Result<IEnumerable<SpecialityPriorityNoteVM>>> GetPriority(string access_token, string access_token_secret)
        {
            var userId = GetUserId(access_token, access_token_secret);
            return Result.Ok(await noteService.GetSpecialityPriorityNotesAsync(Convert.ToInt32(userId)));
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

        private string GetUserId(string access_token, string access_token_secret)
        {
            HttpResponseMessage responseMessageUserId = userService.GetCurrentUserId(access_token, access_token_secret);

            if (responseMessageUserId.IsSuccessStatusCode)
            {
                string resultUserId = responseMessageUserId.Content.ReadAsStringAsync().Result;
                JObject jUserId = JObject.Parse(resultUserId);

                if (jUserId.Count > 0)
                {
                    return jUserId["id"]!.ToString();
                }
            }

            throw new BadHttpRequestException("authorization error: " + responseMessageUserId.ReasonPhrase);
        }
    }
}
