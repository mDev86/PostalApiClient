using MyCSharp.HttpUserAgentParser;

namespace PostalApiClient.v1.Models.MessageInfos;

public class MessageClick
{
    private HttpUserAgentInformation? _userAgentInformation;
    
    /// <summary>
    /// The Url
    /// </summary>
    public string Url { get; init; }

    /// <summary>
    /// The ip address
    /// </summary>
    public string IpAddress { get; init; }

    /// <summary>
    /// The user agent
    /// </summary>
    public string? UserAgent { get; init; }

    /// <summary>
    /// The timestamp
    /// </summary>
    public DateTime? Timestamp { get; init; }
    
    /// <summary>
    /// Parsed user agent information
    /// </summary>
    public HttpUserAgentInformation? UserAgentInfo
    {
        get
        {
            if (_userAgentInformation == null && !string.IsNullOrWhiteSpace(UserAgent))
            {
                _userAgentInformation = HttpUserAgentParser.Parse(UserAgent);
            }
            return _userAgentInformation!.Value;
        }
    }
}