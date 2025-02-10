using System.Text.Json.Serialization;
using MyCSharp.HttpUserAgentParser;

namespace PostalApiClient.v1.Models.Webhook.EventPayloads;

public class MessageClickEvent
{
    private HttpUserAgentInformation? _userAgentInformation;
    
    /// <summary>
    /// Url
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
    
    /// <summary>
    /// Token
    /// </summary>
    [JsonPropertyName("token")]
    public string? Token { get; set; }

    /// <summary>
    /// IP Address
    /// </summary>
    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }
    
    /// <summary>
    /// Message
    /// </summary>
    [JsonPropertyName("message")]
    public WebhookPayloadMessage? Message { get; init; }
    
    /// <summary>
    /// User Agent
    /// </summary>
    [JsonPropertyName("user_agent" )]
    public string? UserAgent { get; set; }
    
    /// <summary>
    /// Parsed user agent information
    /// </summary>
    public HttpUserAgentInformation? UserAgentInfo
    {
        get
        {
            if (_userAgentInformation == null && !string.IsNullOrWhiteSpace(UserAgent))
            {
                _userAgentInformation = HttpUserAgentParser.Parse(UserAgent!);
            }
            return _userAgentInformation;
        }
    }
}