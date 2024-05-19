using Newtonsoft.Json;
using System.Security.Claims;
using D1TechTestCase.Core.Services;

namespace D1TechTestCase.API.Middlewares
{
    public class SecurityStampValidatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _service;
        public SecurityStampValidatorMiddleware(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _service = userService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = Guid.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var tokenSecurityStamp = context.User.FindFirstValue("SecurityStamp");

                var user = await _service.GetUserById(userId);
                

                if (tokenSecurityStamp != user.SecurityStamp)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;


                    context.Response.ContentType = "application/json";
                    var response = new { message = "Please log in again" };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    return;  
                }
            }

            await _next(context);
        }
    }
}
