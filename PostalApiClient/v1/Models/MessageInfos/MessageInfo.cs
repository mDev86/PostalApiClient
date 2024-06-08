using System.Text.Json;
using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.MessageInfos;

/// <summary>
/// The message data.
/// </summary>
public class MessageInfo
{
    private IDictionary<string, ICollection<string>>? _headers;
    
    /// <summary>
    /// Internal message identifier
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; init; }

    /// <summary>
    /// Status
    /// </summary>
    public MessageStatus Status { get; init; }

    /// <summary>
    /// Details
    /// </summary>
    public MessageDetail Details { get; init; }

    /// <summary>
    /// Inspection
    /// </summary>
    public MessageInspection? Inspection { get; init; }

    /// <summary>
    /// Plain_body
    /// </summary>
    public string? PlainBody { get; init; }

    /// <summary>
    /// Html_body
    /// </summary>
    public string? HtmlBody { get; init; }

    /// <summary>
    /// Attachments
    /// </summary>
    public List<MessageAttachment> Attachments { get; init; }

    /// <summary>
    /// All message headers 
    /// </summary>
    [JsonPropertyName("headers")]
    public IDictionary<string, ICollection<string>>? RawHeaders
    {
        get => _headers;
        init
        {
            _headers = value;
            CommonHeaders = JsonSerializer.Deserialize<MessageHeader>(JsonSerializer.Serialize(value));
        }
    }
    
    /// <summary>
    /// Common named message headers
    /// </summary>
    public MessageHeader? CommonHeaders { get; private init; }

    /// <summary>
    /// Raw_message
    /// </summary>
    public string? RawMessage { get; init; }

    /// <summary>
    /// Activity entries
    /// </summary>
    public MessageActivityEntry? ActivityEntries { get; init; }
}