using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PostalApiClient.Utilities;

namespace PostalApiClient.Mvc.Extensions;

public static class Extensions
{
    /// <summary>
    /// Check postal webhook signature
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public static async Task<bool> CheckPostalSignatureAsync(this HttpContext httpContext)
    {
        var signatureVerifier = httpContext.RequestServices.GetRequiredService<PostalWebhookVerifier>();
        return await signatureVerifier.IsSignatureVerifiedAsync(httpContext.Request);
    }
}