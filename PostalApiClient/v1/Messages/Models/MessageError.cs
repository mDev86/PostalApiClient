using PostalApiClient.v1.Models;

namespace PostalApiClient.v1.Messages.Models;

/// <summary>
/// Error by GetMessage operation
/// </summary>
public class MessageError: BaseError
{
    /// <summary>
    /// Internal id of the message
    /// </summary>
    public int? Id { get; init; }
}