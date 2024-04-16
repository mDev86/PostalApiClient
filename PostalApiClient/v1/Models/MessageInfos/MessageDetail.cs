namespace PostalApiClient.v1.Models.MessageInfos;

public class MessageDetail
{
    private DateTime? _timestampDateTime;

    /// <summary>
    /// The e-mail addresses of the recipients.
    /// </summary>
    public string RcptTo { get; init; }

    /// <summary>
    /// The from email address.
    /// </summary>
    public string MailFrom { get; init; }

    /// <summary>
    /// The message subject
    /// </summary>
    public string Subject { get; init; }

    /// <summary>
    /// The message id string.
    /// </summary>
    public string MessageId { get; init; }

    /// <summary>
    /// The timestamp of the message.
    /// </summary>
    public double? Timestamp { get; init; }

    /// <summary>
    /// The timestamp of the message converted to DateTime.
    /// </summary>
    public DateTime? TimestampDateTime
    {
        get
        {
            if (_timestampDateTime == null && Timestamp.HasValue)
            {
                _timestampDateTime = DateTimeOffset.FromUnixTimeSeconds((long)Timestamp.Value).DateTime;
            }
            return _timestampDateTime;
        }
    }

    /// <summary>
    /// The direction of the message.
    /// </summary>
    public MessageDirection Direction { get; init; }

    /// <summary>
    /// The size of the message (Bytes)
    /// </summary>
    public string Size { get; init; }

    /// <summary>
    /// The bounces for the message
    /// </summary>
    public bool Bounce { get; init; }

    /// <summary>
    /// The bounces for the Id 
    /// </summary>
    public int BounceForId { get; init; }

    /// <summary>
    /// The tag for the message
    /// </summary>
    public string Tag { get; init; }

    public bool ReceivedWithSSL { get; init; }
}