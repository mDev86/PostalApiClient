using PostalApiClient.v1.Models;

namespace PostalApiClient.v1.Sends.Models;

public class SendError: BaseError
{
    public dynamic? Errors { get; init; }
}