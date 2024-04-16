using System.Text.Json.Serialization;
using PostalApiClient.v1.Models.MessageInfos;

namespace PostalApiClient.v1.Models.Webhook;

/// <summary>
/// The webhook event payload message
/// </summary>
public class WebhookPayloadMessage
{
    private DateTime? _timestampDateTime;

    /// <summary>
    /// The message identifier int
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// The message token
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; }

    /// <summary>
    /// The message direction
    /// </summary>
    [JsonPropertyName("direction")]
    public MessageDirection Direction { get; set; }

    /// <summary>
    /// The message id string
    /// </summary>
    [JsonPropertyName("message_id" )]
    public string MessageId { get; set; }

    /// <summary>
    /// The e-mail addresses of the recipients
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; set; }

    /// <summary>
    /// The from email address.
    /// </summary>
    [JsonPropertyName("from")]
    public string From { get; set; }

    /// <summary>
    /// The message subject
    /// </summary>
    [JsonPropertyName("subject")]
    public string Subject { get; set; }

    /// <summary>
    /// The spam status for the message
    /// </summary>
    [JsonPropertyName("spam_status")]
    public MessageSpamCheck SpamStatus { get; set; }

    /// <summary>
    /// The tag for the message
    /// </summary>
    [JsonPropertyName("tag")]
    public string? Tag { get; set; }
    
    /// <summary>
    /// The timestamp of the message
    /// </summary>
    [JsonPropertyName("timestamp")]
    public double? Timestamp { get; set; }

    /// <summary>
    /// The timestamp of the message converted to DateTime
    /// </summary>
    public DateTime? TimestampDateTime
    {
        get
        {
            if ( _timestampDateTime == null && Timestamp != null )
            {
                _timestampDateTime = DateTimeOffset.FromUnixTimeSeconds((long) Timestamp.Value).DateTime;
            }
            return _timestampDateTime;
        }
    }
}