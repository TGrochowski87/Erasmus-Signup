using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UniversityApi.Controllers;
using UniversityApi.Utilities;

namespace UniversityApi.Attributes
{
    public class AuthorizeUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorization);

            if (string.IsNullOrEmpty(authorization))
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                var token = authorization.ToString()
                .Substring("Bearer ".Length)
                .Trim();

                var userToken = OAuthTool.DecodeToken(token);

                if (!userToken.IsSuccess)
                {
                    context.Result = new UnauthorizedResult();
                }
                else
                {
                    ((IUniversityApiController)context.Controller).UserToken = userToken;
                    base.OnActionExecuting(context);
                }
            }

        }
    }
}
