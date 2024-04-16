using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.Webhook.EventPayloads;

public class MessageBounceEvent
{
    /// <summary>
    /// Original message
    /// </summary>
    [JsonPropertyName("original_message")]
    public WebhookPayloadMessage OriginalMessage { get; init; }

    /// <summary>
    /// Bounce message
    /// </summary>
    [JsonPropertyName("bounce")]
    public WebhookPayloadMessage Bounce { get; init; }
}