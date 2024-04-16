using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models;

public enum Status
{
    Success = 1,
    
    [JsonPropertyName("parameter-error")]
    ParameterError = 2,
    
    // [JsonPropertyName("error")]
    Error = 3
}