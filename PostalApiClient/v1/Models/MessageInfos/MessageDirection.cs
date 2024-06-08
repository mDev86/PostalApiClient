using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.MessageInfos;

/// <summary>
/// Type of message direction
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MessageDirection
{
    Incoming,
    Outgoing
}