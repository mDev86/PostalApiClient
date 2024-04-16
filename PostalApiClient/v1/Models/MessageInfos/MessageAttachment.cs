using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.MessageInfos;

public class MessageAttachment
{
    [JsonPropertyName("filename")]
    public string FileName { get; init; }

    // [JsonPropertyName("content_type")]
    public string ContentType { get; init; }

    /// <summary>
    /// Content in base64
    /// </summary>
    public string Data { get; init; }
    
    /// <summary>
    /// Byte size
    /// </summary>
    public int Size { get; init; }

    /// <summary>
    /// SHA1
    /// </summary>
    public string Hash { get; init; }
}