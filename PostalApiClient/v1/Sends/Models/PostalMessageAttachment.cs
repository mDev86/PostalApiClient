using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Sends.Models;

public class PostalMessageAttachment
{
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the content type.
    /// </summary>
    [JsonPropertyName("content_type")]
    public string ContentType { get; set; }

    /// <summary>
    /// Gets or sets the base64 encoded data.
    /// </summary>
    public string Data { get; set; }
}