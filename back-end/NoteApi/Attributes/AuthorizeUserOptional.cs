using Microsoft.AspNetCore.Mvc.Filters;
using NoteApi.Controllers;
using NoteApi.Utilities;

namespace NoteApi.Attributes
{
    public class AuthorizeUserOptional : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorization);

            if (!string.IsNullOrEmpty(authorization))
            {
                var token = authorization.ToString()
                .Substring("Bearer ".Length)
                .Trim();

                var userToken = OAuthTool.DecodeToken(token);

                if (userToken.IsSuccess)
                {
                    ((INoteController)context.Controller).UserToken = userToken;
                    base.OnActionExecuting(context);
                }
            }

        }
    }
}
