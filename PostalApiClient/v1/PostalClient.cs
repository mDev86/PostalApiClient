using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using OneOf;
using PostalApiClient.Utilities;
using PostalApiClient.v1.Models;

namespace PostalApiClient.v1;

/// <summary>
/// Client for send message to PostalServer
/// </summary>
public partial class PostalClient
{
    /// <summary>
    /// The http client
    /// </summary>
    private readonly HttpClient _httpClient;
        
    /// <summary>
    /// Serializer options to dto models
    /// </summary>
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public PostalClient(HttpClient httpClient, IOptions<Options> options)
    {
        httpClient.BaseAddress = new Uri($"{options.Value.Server}/api/v1/");
        // Add authorization header
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Server-API-Key", options.Value.ApiKey);
        
        _httpClient = httpClient;
        
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new CustomSnakeCseLowerPolicy(),
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = { new JsonStringEnumMemberConverter() }
        };
    }
    
    /// <summary>
    /// Executes the specified request
    /// </summary>
    /// <param name="uri">Uri to action</param>
    /// <param name="bodyObject">Object with body parameters</param>
    /// <typeparam name="TSuccess">Response model type for success action</typeparam>
    /// <typeparam name="TError">Response model type for error action</typeparam>
    /// <returns></returns>
    private async Task<OneOf<TSuccess, TError>> ExecuteAsync<TSuccess, TError>(string uri, object bodyObject) 
        where TError: BaseError, new()
    {
        HttpResponseMessage response;
        try
        {
            
            var content = new StringContent(JsonSerializer.Serialize(bodyObject, _jsonSerializerOptions),
                Encoding.UTF8,
                "application/json");
            response = await _httpClient.PostAsync(uri, content);
        }
        catch (Exception e)
        {
            return 
                new TError
                {
                    Code = ErrorCode.ServerUnavailable,
                    Message = e.Message
                };
        }

        var responseString = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            return new TError
            {
                Code = ErrorCode.ServerUnavailable,
                Message = $"Postal server return http status: {(int) response.StatusCode} ({response.StatusCode})"
            };
        }

        var baseResponse = JsonSerializer.Deserialize<BaseResponse>(responseString, _jsonSerializerOptions);
        
        return baseResponse!.Status != Status.Success
            ? baseResponse.Data.Deserialize<TError>(_jsonSerializerOptions)
            : baseResponse.Data.Deserialize<TSuccess>(_jsonSerializerOptions);
    }
}