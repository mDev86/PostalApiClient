using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using PostalApiClient.v1.Models.Webhook;

namespace PostalApiClient.Utilities;

/// <summary>
/// Request verifier from Postal webhook 
/// </summary>
public class PostalWebhookVerifier
{
    private const string SignatureHeaderName = "X-Postal-Signature-256";
    private readonly RSA _rsa;
    
    public PostalWebhookVerifier(IOptions<Options> options)
    {
        if (options.Value.DkimP == null)
        {
            throw new OptionsValidationException("PostalClient.DkimPublicKey", typeof(string), new[] {"Dkim public key required for postal webhook verifier"});
        }

        _rsa = CreateRSAFromFromPublicKey(options.Value.DkimP);
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
        => _rsa.VerifyData(
            Encoding.UTF8.GetBytes(payload),
            Convert.FromBase64String(signature256),
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);
    
    private bool IsSignatureVerified(string payload, IHeaderDictionary requestHeaders)
        => requestHeaders.TryGetValue(SignatureHeaderName, out var signature) &&
           IsSignatureVerified(payload, signature!);
    
    
    private RSA CreateRSAFromFromPublicKey(string dkim)
    {
        AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(Convert.FromBase64String(dkim));
        RsaKeyParameters rsaKeyParameters = (RsaKeyParameters) asymmetricKeyParameter;
        var rsaParameters = new RSAParameters
        {
            Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned(),
            Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
        };
        var rsa = new RSACryptoServiceProvider();
        rsa.ImportParameters(rsaParameters);

        return rsa;
    }
}