namespace PostalApiClient.v1.Sends.Models;

/// <summary>
/// Model for send message by Postal server
/// </summary>
public class PostalMessage
{
    /// <summary>
    /// An array of emails addresses
    /// </summary>
    public ICollection<string> To { get; set; }

    /// <summary>
    ///  An array of email addresses to CC
    /// </summary>
    public ICollection<string>? Cc { get; set; }

    /// <summary>
    /// An array of email addresses to BCC
    /// </summary>
    public ICollection<string>? Bcc { get; set; }

    /// <summary>
    /// The name/email to send the email from
    /// </summary>
    public string From { get; set; }

    /// <summary>
    /// The name/email of the 'Sender'
    /// </summary>
    public string? Sender { get; set; }

    /// <summary>
    /// The subject of message
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// A custom tag to add to the message
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// The name/email of the 'Reply-to'
    /// </summary>
    public string? ReplyTo { get; set; }

    /// <summary>
    /// The plain body
    /// </summary>
    public string? PlainBody { get; set; }

    /// <summary>
    /// The HTML body
    /// </summary>
    public string? HtmlBody { get; set; }

    /// <summary>
    /// An array of attachments
    /// </summary>
    public ICollection<PostalMessageAttachment>? Attachments { get; set; }

    /// <summary>
    /// Custom headers
    /// </summary>
    public IDictionary<string, string>? Headers { get; set; }

    /// <summary>
    /// Is this message a bounce?
    /// </summary>
    public bool? Bounce { get; set; }
}