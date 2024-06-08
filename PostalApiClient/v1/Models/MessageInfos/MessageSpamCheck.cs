using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.MessageInfos;

/// <summary>
/// Spam checker result
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum MessageSpamCheck
{
    NotChecked,
    Spam,
    NotSpam
}