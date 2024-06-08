namespace PostalApiClient.v1.Sends.Models;

/// <summary>
/// Model for send raw message by Postal server
/// </summary>
public class PostalRawMessage
{
    /// <summary>
    /// The address that should be logged as sending the message
    /// </summary>
    public string MailFrom { get; set; }

    /// <summary>
    /// The addresses this message should be sent to
    /// </summary>
    public ICollection<string> RcptTo { get; set; }

    /// <summary>
    /// The base64 encoded RFC2822 message to send
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// Is this message a bounce?
    /// </summary>
    public bool? Bounce { get; set; }
}