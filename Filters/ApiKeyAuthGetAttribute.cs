using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;



namespace WebAPICallSP.Filters
{
    [AttributeUsage(validOn: AttributeTargets.Method)]
    // ApiKeyAuthAttribute is the [ApiKeyAuth] an attribute class
    public class ApiKeyAuthGetAttribute: Attribute, IAsyncActionFilter
    {
             
        private const string ApiKeyHeaderName = "ApiKeyGet";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apikey = configuration.GetValue<string>(key: "ApiKeyGet");

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
