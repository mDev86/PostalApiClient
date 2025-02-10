using OneOf;
using PostalApiClient.v1.Messages.Models;
using PostalApiClient.v1.Models.MessageInfos;

namespace PostalApiClient.v1;

public partial class PostalClient
{
    /// <summary>
    /// Get all details about a message.
    /// </summary>
    /// <param name="internalMessageId">The internal id of the message from MessageInfo</param>
    /// <param name="expansions">The expansions to include in the request.</param>
    /// <returns></returns>
    /// <remarks>
    /// If set Expansions = true raised Internal exception on server (500).
    /// Therefore, we transform all available values into a collection
    /// </remarks>
    public async Task<OneOf<MessageInfo, MessageError>> GetMessageDetailsAsync(int internalMessageId, MessageExpansion? expansions = null)
        => await ExecuteAsync<MessageInfo, MessageError>("messages/message",
            new
            {
                Id = internalMessageId,
                Expansions = Enum.GetValues(typeof(MessageExpansion))
                    .Cast<MessageExpansion>()
                    .Where(v => expansions?.HasFlag(v) == true)
            });

    /// <summary>
    /// Get the deliveries which have been attempted for this message.
    /// </summary>
    /// <param name="internalMessageId">The internal id of the message from MessageInfo</param>
    /// <returns></returns>
    public async Task<OneOf<ICollection<MessageDeliveryResponse>, MessageError>> GetMessageDeliveriesAsync(int internalMessageId)
        => await ExecuteAsync<ICollection<MessageDeliveryResponse>, MessageError>("messages/deliveries",
            new {Id = internalMessageId});
}