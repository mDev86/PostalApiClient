using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PostalApiClient.v1.Models.Webhook;

namespace PostalApiClient.Utilities;

/// <summary>
/// Request verifier from Postal webhook 
/// </summary>
public class PostalWebhookVerifier
{
    private const string SignatureHeaderName = "X-Postal-Signature-256";
    private readonly byte[] dkim;
    
    public PostalWebhookVerifier(IOptions<Options> options)
    {
        if (options.Value.DkimP == null)
        {
            throw new OptionsValidationException("PostalClient.DkimPublicKey", typeof(string), new[] {"Dkim public key required for postal webhook verifier"});
        }
        dkim = Convert.FromBase64String(options.Value.DkimP);
    }
    
    /// <summary>
    /// Check postal webhook signature
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> IsSignatureVerifiedAsync(HttpRequest request)
    {
        using var reader = new StreamReader(request.Body, Encoding.UTF8);
        var rawBody = await reader.ReadToEndAsync();

        // Writing body data back to request for normal model binder read
        var rawBodyBytes = Encoding.UTF8.GetBytes(rawBody);
        request.Body = new MemoryStream(rawBodyBytes);

        return IsSignatureVerified(rawBody, request.Headers);
    }

    /// <summary>
    /// Check postal webhook payload signature
    /// </summary>
    /// <param name="payload">Webhook model</param>
    /// <param name="requestHeaders">Headers from reuqest</param>
    /// <returns></returns>
    public bool IsSignatureVerified(PostalWebhook payload, IHeaderDictionary requestHeaders)
        => requestHeaders.TryGetValue(SignatureHeaderName, out var signature) &&
           IsSignatureVerified(JsonSerializer.Serialize(payload), signature!);
    
    /// <summary>
    /// Check postal webhook payload signature
    /// </summary>
    /// <param name="payload">Webhook model</param>
    /// <param name="signature256">Payload signature</param>
    /// <returns></returns>
    /// <exception cref="CryptographicException"></exception>
    public bool IsSignatureVerified(string payload, string signature256)
    {
        var rsa = PublicKey.CreateFromSubjectPublicKeyInfo(dkim, out _)
            .GetRSAPublicKey();
        
        if (rsa is null)
        {
            throw new CryptographicException("Public key is are not RSA key");
        }

        return rsa.VerifyData(
            Encoding.UTF8.GetBytes(payload),
            Convert.FromBase64String(signature256),
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);
    }
    
    private bool IsSignatureVerified(string payload, IHeaderDictionary requestHeaders)
        => requestHeaders.TryGetValue(SignatureHeaderName, out var signature) &&
           IsSignatureVerified(payload, signature!);
}