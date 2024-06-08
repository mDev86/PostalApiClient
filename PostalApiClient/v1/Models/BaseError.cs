namespace PostalApiClient.v1.Models;

/// <summary>
/// The common error message data
/// </summary>
public class BaseError
{
    /// <summary>
    /// Gets or sets the error code
    /// </summary>
    public ErrorCode Code { get; init; }

    /// <summary>
    /// Gets or sets the messages
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// The token that was looked up. Fill for error <see cref="ErrorCode.InvalidServerAPIKey"/>
    /// </summary>
    public string? Token { get; init; }
}