using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.Webhook;

/// <summary>
/// Type of webhook event
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter))]
[Flags]
public enum WebhookEvent
{
    /// <summary>
    /// An e-mail has been successfully delivered to its endpoint (either SMTP or HTTP)
    /// </summary>
    MessageSent = 1,

    /// <summary>
    /// An e-mail has been delayed due to an issue with the receiving endpoint. It will be retried automatically
    /// </summary>
    MessageDelayed = 2,

    /// <summary>
    /// An e-mail cannot be delivered to its endpoint. This is a permanent failure so it will no be retried
    /// </summary>
    MessageDeliveryFailed = 4,

    /// <summary>
    /// An e-mail has been held in Postal. This will be because a limit has been reached or your server is in development mode
    /// </summary>
    MessageHeld = 8,

    /// <summary>
    /// We received a bounce message in response to an email which had previously been successfully sent
    /// </summary>
    MessageBounced = 16,

    /// <summary>
    /// A link in one of your outbound messages has been clicked
    /// </summary>
    MessageLinkClicked = 32,

    /// <summary>
    /// A message you have sent has been loaded
    /// </summary>
    MessageLoaded = 64,

    /// <summary>
    /// This will be triggered when we detect an issue with the DNS configuration for any domain for this server
    /// </summary>
    DomainDNSError = 128,

    MessageStatusEvent = MessageSent | MessageDelayed | MessageDeliveryFailed | MessageHeld
}