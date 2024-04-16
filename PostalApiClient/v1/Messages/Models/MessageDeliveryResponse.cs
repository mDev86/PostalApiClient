using PostalApiClient.v1.Models.MessageInfos;

namespace PostalApiClient.v1.Messages.Models;

/// <summary>
/// The message data.
/// </summary>
public class MessageDeliveryResponse
{
    private DateTime? _timestampDateTime;

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public DeliveryStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the details.
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// Gets or sets the output.
    /// </summary>
    public string? Output { get; set; }

    /// <summary>
    /// Gets or sets the sent_with_ssl flag.
    /// </summary>
    public bool? SentWithSsl { get; set; }

    /// <summary>
    /// Gets or sets the log_id.
    /// </summary>
    public string LogId { get; set; }

    /// <summary>
    /// Gets or sets the delivery time.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public double? Time { get; set; }

    /// <summary>
    /// Gets or sets the timestamp.
    /// </summary>
    public double? Timestamp { get; set; }

    /// <summary>
    /// The timestamp of the detail converted to DateTime.
    /// </summary>
    public DateTime? TimestampDateTime
    {
        get
        {
            if (!_timestampDateTime.HasValue && Timestamp.HasValue)
            {
                _timestampDateTime = DateTimeOffset.FromUnixTimeSeconds((long)Timestamp.Value).DateTime;
            }
            return _timestampDateTime;
        }
    }
}