using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models;

/// <summary>
/// Operation request status 
/// </summary>
public enum Status
{
    Success = 1,
    
    [JsonPropertyName("parameter-error")]
    ParameterError = 2,

    Error = 3
}