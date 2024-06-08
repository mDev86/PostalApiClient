using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.Webhook.EventPayloads;

/// <summary>
/// DNS checker result
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum DNSCheckStatus
{
    OK,
    Missing,
    Invalid
}