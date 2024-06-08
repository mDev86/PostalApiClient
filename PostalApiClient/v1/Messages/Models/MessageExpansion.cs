using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Messages.Models;

/// <summary>
/// Extended message data for include request 
/// </summary>
[Flags]
public enum MessageExpansion
{
    [JsonPropertyName("status")]
    Status = 1,
    
    [JsonPropertyName("details")]
    Details = 2,
    
    [JsonPropertyName("inspection")]
    Inspection = 4,
    
    [JsonPropertyName("plain_body")]
    PlainBody = 8,
    
    [JsonPropertyName("html_body")]
    HtmlBody = 16,
    
    [JsonPropertyName("attachments")]
    Attachments = 32,
    
    [JsonPropertyName("headers")]
    Headers = 64,
    
    [JsonPropertyName("raw_message")]
    RawMessage = 128,

    /// <summary>
    /// Include all data for message
    /// </summary>
    All = Status | Details | Inspection | PlainBody | HtmlBody | Attachments | Headers | RawMessage
}