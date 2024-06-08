using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.MessageInfos;

/// <summary>
/// Message delivery status
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum DeliveryStatus
{
    Sent,
    Pending,
    Processed,
    Held,
    SoftFail,
    HardFail,
    Bounced,
    HoldCancelled,
    Error
}