using PostalApiClient.v1.Models;

namespace PostalApiClient.v1.Sends.Models;

/// <summary>
/// Error by SendMessage operation
/// </summary>
public class SendError: BaseError
{
    public dynamic? Errors { get; init; }
}