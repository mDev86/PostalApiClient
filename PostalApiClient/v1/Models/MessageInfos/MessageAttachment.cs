using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.MessageInfos;

/// <summary>
/// Attachment
/// </summary>
public class MessageAttachment
{
    /// <summary>
    /// Name of attached file
    /// </summary>
    [JsonPropertyName("filename")]
    public string FileName { get; init; }

    /// <summary>
    /// Content type
    /// </summary>
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