using System.Text.Json.Serialization;
using PostalApiClient.v1.Models.MessageInfos;

namespace PostalApiClient.v1.Models.Webhook.EventPayloads;

public class MessageStatusEvent
{
    private DateTime? _timestampDateTime;
    
    /// <summary>
    /// Message
    /// </summary>
    [JsonPropertyName("message")]
    public WebhookPayloadMessage? Message { get; init; }

    /// <summary>
    /// Delivery status
    /// </summary>
    [JsonPropertyName("status")]
    public DeliveryStatus Status { get; init; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonPropertyName("details")]
    public string Details { get; init; }

    /// <summary>
    /// Output
    /// </summary>
    [JsonPropertyName("output")]
    public string Output { get; init; }

    /// <summary>
    /// Sent_with_ssl property
    /// </summary>
    [JsonPropertyName("sent_with_ssl")]
    public bool? SentWithSsl { get; init; }
    
    /// <summary>
    /// Time
    /// </summary>
    [JsonPropertyName("time")]
    public double? Time { get; init; }
    
    /// <summary>
    /// The timestamp of the payload
    /// </summary>
    [JsonPropertyName("timestamp")]
    public double? Timestamp { get; init; }

    /// <summary>
    /// The timestamp of the payload converted to DateTime
    /// </summary>
    public DateTime? TimestampDateTime
    {
        get
        {
            if (_timestampDateTime == null && Timestamp != null)
            {
                _timestampDateTime = DateTimeOffset.FromUnixTimeSeconds((long) Timestamp.Value).DateTime;
            }

            return _timestampDateTime;
        }
    }
}