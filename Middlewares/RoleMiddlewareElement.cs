using Microsoft.AspNetCore.Authorization;

namespace MovieTicketingApplication.Middlewares
{
    public class RoleMiddlewareElement
    {
        private readonly RequestDelegate _next;

        public RoleMiddlewareElement(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var user = context.User;

            if (user.Identity.IsAuthenticated)
            {
                var roles = user.Claims
                                .Where(c => c.Type == "role")
                                .Select(c => c.Value)
                                .ToList();

                var endpoint = context.GetEndpoint();
                if(endpoint != null )
                {
                    var authorizationAttributes = endpoint.Metadata.GetOrderedMetadata<AuthorizeAttribute>();
                    if(authorizationAttributes != null)
                    {
                        foreach( var attribute in authorizationAttributes)
                        {
                            if (!roles.Contains(attribute.Roles))
                            {
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                return;
                            }
                        }
                    }
                }
                
                await _next(context);

            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}
