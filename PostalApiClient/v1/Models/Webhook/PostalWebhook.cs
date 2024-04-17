using System.Text.Json;
using System.Text.Json.Serialization;
using PostalApiClient.v1.Models.Webhook.EventPayloads;

namespace PostalApiClient.v1.Models.Webhook;

/// <summary>
/// The webhook event data
/// </summary>
public class PostalWebhook
{
    private DateTime? _timestampDateTime;
    private JsonElement _payload;

    /// <summary>
    /// Event
    /// </summary>
    [JsonPropertyName("event")]
    public WebhookEvent Event { get; init; }

    /// <summary>
    /// The timestamp of the event
    /// </summary>
    [JsonPropertyName("timestamp")]
    public double? Timestamp { get; init; }

    /// <summary>
    /// Raw payload
    /// </summary>
    [JsonPropertyName("payload")]
    public JsonElement RawPayload
    {
        get => _payload;
        init 
        {
            _payload = value;
            if (WebhookEvent.MessageStatusEvent.HasFlag(Event))
            {
                MessageStatusEvent = value.Deserialize<MessageStatusEvent>();
            }
            else if (WebhookEvent.MessageBounced.HasFlag(Event))
            {
                MessageBounceEvent = value.Deserialize<MessageBounceEvent>();
            }
            else if (WebhookEvent.MessageLinkClicked.HasFlag(Event))
            {
                MessageClickEvent = value.Deserialize<MessageClickEvent>();
            }
            else if (WebhookEvent.MessageLoaded.HasFlag(Event))
            {
                MessageLoadedEvent = value.Deserialize<MessageLoadedEvent>();
            }
            else if (WebhookEvent.DomainDNSError.HasFlag(Event))
            {
                DNSErrorEvent = value.Deserialize<DNSErrorEvent>();
            }
        }
    }

    /// <summary>
    /// Filled if <see cref="Event"/> equal <see cref="WebhookEvent.MessageStatusEvent"/>
    /// </summary>
    [JsonIgnore]
    public MessageStatusEvent? MessageStatusEvent { get; private init; }
    
    /// <summary>
    /// Filled if <see cref="Event"/> equal <see cref="WebhookEvent.MessageBounced"/>
    /// </summary>
    [JsonIgnore]
    public MessageBounceEvent? MessageBounceEvent { get; private init; }
    
    /// <summary>
    /// Filled if <see cref="Event"/> equal <see cref="WebhookEvent.MessageLinkClicked"/>
    /// </summary>
    [JsonIgnore]
    public MessageClickEvent? MessageClickEvent { get; private init; }
    
    /// <summary>
    /// Filled if <see cref="Event"/> equal <see cref="WebhookEvent.MessageLoaded"/>
    /// </summary>
    [JsonIgnore]
    public MessageLoadedEvent? MessageLoadedEvent { get; private init; }
    
    /// <summary>
    /// Filled if <see cref="Event"/> equal <see cref="WebhookEvent.DomainDNSError"/>
    /// </summary>
    [JsonIgnore]
    public DNSErrorEvent? DNSErrorEvent { get; private init; }
    
    /// <summary>
    /// Event uuid
    /// </summary>
    [JsonPropertyName("uuid")]
    public Guid Uuid { get; init; }
    
    /// <summary>
    /// The timestamp of the event converted to DateTime.
    /// </summary>
    [JsonIgnore]
    public DateTime? TimestampDateTime
    {
        get
        {
            if (!_timestampDateTime.HasValue && Timestamp.HasValue)
            {
                _timestampDateTime = DateTimeOffset.FromUnixTimeSeconds((long) Timestamp.Value).DateTime;
            }

            return _timestampDateTime;
        }
    }
}