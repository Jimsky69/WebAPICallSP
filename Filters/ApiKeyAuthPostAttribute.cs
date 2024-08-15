using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebAPICallSP.Filters
{
   public class ApiKeyAuthPostGetAttribute : Attribute, IAsyncActionFilter
        {

            private const string ApiKeyHeaderName = "ApiKeyPost";

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                var apikey = configuration.GetValue<string>(key: "ApiKeyPost");

                if (!apikey.Equals(potentialApiKey))
                {
                    context.Result = new UnauthorizedResult();
                    return;

                }
                // jf after the await next() below, it means it will go the controller. So the above validation
                // will validate the key first b4 it goes to the controller
                await next();

            }
        }
}

