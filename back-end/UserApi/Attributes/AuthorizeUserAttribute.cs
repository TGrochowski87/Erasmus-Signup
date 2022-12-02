using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserApi.Utilities;

namespace UserApi.Attributes
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

                var decodeToken = OAuthTool.DecodeToken(token);

                if (!decodeToken.IsSuccess)
                {
                    context.Result = new EmptyResult();
                }
                else
                    base.OnActionExecuting(context);
            }

        }
    }
}
