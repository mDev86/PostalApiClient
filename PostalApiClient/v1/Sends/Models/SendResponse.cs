using PostalApiClient.v1.Models.MessageInfos;

namespace PostalApiClient.v1.Sends.Models;

/// <summary>
/// The message data
/// </summary>
public class SendResponse
{
    /// <summary>
    /// Message id
    /// </summary>
    public string MessageId { get; init; }

    /// <summary>
    /// Sended messages
    /// Key: reciever\to email 
    /// </summary>
    public IDictionary<string, MessageInfo> Messages { get; init; }
}