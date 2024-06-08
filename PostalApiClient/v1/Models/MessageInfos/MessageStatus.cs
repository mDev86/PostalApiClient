namespace PostalApiClient.v1.Models.MessageInfos;

/// <summary>
/// The message status details
/// </summary>
public class MessageStatus
{
    private DateTime? _lastDeliveryAttemptDateTime;
    private DateTime? _holdExpiryDateTime;

    /// <summary>
    /// Gets or sets the status
    /// </summary>
    public DeliveryStatus Status { get; init; }

    /// <summary>
    /// Gets or sets the held
    /// </summary>
    public bool? Held { get; init; }

    /// <summary>
    /// Gets or sets the hold expiry
    /// </summary>
    public double? HoldExpiry { get; init; }

    /// <summary>
    /// Gets or sets the last delivery attempt in UnixTimeSeconds
    /// </summary>
    public double? LastDeliveryAttempt { get; init; }
    
    /// <summary>
    /// Gets or sets the last delivery attempt DateTime
    /// </summary>
    public DateTime? LastDeliveryAttemptDateTime
    {
        get 
        {
            if (!_lastDeliveryAttemptDateTime.HasValue && LastDeliveryAttempt.HasValue ) 
            {
                _lastDeliveryAttemptDateTime = DateTimeOffset.FromUnixTimeSeconds((long)LastDeliveryAttempt.Value).DateTime;
            }
            return _lastDeliveryAttemptDateTime;
        }
    }
    
    /// <summary>
    /// Gets or sets the last delivery attempt DateTime
    /// </summary>
    public DateTime? HoldExpiryDateTime
    {
        get 
        {
            if (!_holdExpiryDateTime.HasValue && HoldExpiry.HasValue ) 
            {
                _holdExpiryDateTime = DateTimeOffset.FromUnixTimeSeconds((long)HoldExpiry.Value).DateTime;
            }
            return _holdExpiryDateTime;
        }
    }
}