using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.MessageInfos;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MessageDirection
{
    Incoming,
    Outgoing
}