using PostalApiClient.v1.Messages.Models;
using PostalApiClient.v1.Models.MessageInfos;

namespace PostalApiClient.v1;

public partial class PostalClient
{
    /// <summary>
    /// Get all details about a message.
    /// </summary>
    /// <param name="messageId">The id of the message</param>
    /// <param name="expansions">The expansions to include in the request.</param>
    /// <returns></returns>
    /// <remarks>
    /// If set Expansions = true raised Internal exception on server (500).
    /// Therefore, we transform all available values into a collection
    /// </remarks>
    public async Task<(MessageInfo? Result, MessageError? Error)> GetMessageDetailsAsync(int messageId, MessageExpansion? expansions = null)
        => await ExecuteAsync<MessageInfo, MessageError>("messages/message",
            new
            {
                Id = messageId,
                Expansions = Enum.GetValues<MessageExpansion>()
                    .Where(v => expansions?.HasFlag(v) == true)
            });

    /// <summary>
    /// Get the deliveries which have been attempted for this message.
    /// </summary>
    /// <param name="messageId">The id of the message</param>
    /// <returns></returns>
    public async Task<(ICollection<MessageDeliveryResponse>? Result, MessageError? Error)> GetMessageDeliveriesAsync(int messageId)
        => await ExecuteAsync<ICollection<MessageDeliveryResponse>, MessageError>("messages/deliveries",
            new {Id = messageId});
}