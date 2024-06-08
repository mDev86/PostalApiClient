namespace PostalApiClient.v1.Models.MessageInfos;

/// <summary>
/// Message inspection info
/// </summary>
public class MessageInspection
{
    /// <summary>
    /// The inspected flag
    /// </summary>
    public bool Inspected { get; init; }

    /// <summary>
    /// The spam flag
    /// </summary>
    public bool Spam { get; init; }

    /// <summary>
    /// The spam score
    /// </summary>
    public float? SpamScore { get; init; }

    /// <summary>
    /// The threat flag
    /// </summary>
    public bool? Threat { get; init; }

    /// <summary>
    /// The threat setting
    /// </summary>
    public string? ThreatDetails { get; init; }

    /// <summary>
    /// Spam check status
    /// </summary>
    public MessageSpamCheck SpamCheck =>
        Inspected
            ? Spam
                ? MessageSpamCheck.Spam
                : MessageSpamCheck.NotSpam
            : MessageSpamCheck.NotChecked;
}