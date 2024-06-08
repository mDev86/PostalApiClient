using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.MessageInfos;

/// <summary>
/// Standards email headers
/// </summary>
public class MessageHeader
{
    public ICollection<string> Received { get; init; }

    public ICollection<string> Date { get; init; }

    public ICollection<string> From { get; init; }

    public ICollection<string> To { get; init; }

    [JsonPropertyName("message-id")]
    public ICollection<string> MessageId { get; init; }

    public ICollection<string> Subject { get; init; }

    [JsonPropertyName("mime-version")]
    public ICollection<string> MimeVersion { get; init; }

    [JsonPropertyName("content-type")]
    public ICollection<string> ContentType { get; init; }

    [JsonPropertyName("content-transfer-encoding")]
    public ICollection<string> ContentTransferEncoding { get; init; }

    public ICollection<string> DkimSignature { get; init; }

    public ICollection<string> XPostalMsgId { get; init; }
}

