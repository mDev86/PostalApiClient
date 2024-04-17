using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PostalApiClient.Mvc.Extensions;

/// <summary>
/// Attribute for verifying postal webhook signature.
/// Return status 401 Unauthorized If signature not valid
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PostalSignatureVerifyAttribute: Attribute, IAsyncAuthorizationFilter
{   
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var isVerified = await context.HttpContext.CheckPostalSignatureAsync();
        
        if (!isVerified)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}