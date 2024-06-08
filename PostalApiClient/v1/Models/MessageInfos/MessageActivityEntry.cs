namespace PostalApiClient.v1.Models.MessageInfos;

/// <summary>
/// Activity entries
/// </summary>
public class MessageActivityEntry
{
    /// <summary>
    /// The loads for a message
    /// </summary>
    public ICollection<MessageLoad> Loads { get; init; }

    /// <summary>
    /// The clicks for a message
    /// </summary>
    public ICollection<MessageClick> Clicks { get; init; }
}