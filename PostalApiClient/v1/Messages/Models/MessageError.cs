using PostalApiClient.v1.Models;

namespace PostalApiClient.v1.Messages.Models;

public class MessageError: BaseError
{
    /// <summary>
    /// The ID of the message
    /// </summary>
    public int? Id { get; init; }
}