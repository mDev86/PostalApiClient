using System.Text.Json;

namespace PostalApiClient.v1.Models;

/// <summary>
/// The api response retrieving a single message
/// </summary>
public class BaseResponse
{
    /// <summary>
    /// Indication about whether the request was performed successfully
    /// </summary>
    public Status Status { get; init; }

    /// <summary>
    /// How long the request took to complete on the server side
    /// </summary>
    public float Time { get; init; }

    /// <summary>
    /// Additional information about the request
    /// </summary>
    public dynamic Flags { get; init; }
    
    /// <summary>
    /// Data returned from the action
    /// </summary>
    public JsonElement Data { get; init; }
}