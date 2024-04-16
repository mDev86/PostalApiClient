using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models;

/// <summary>
/// All available errors from actions
/// </summary>
[JsonStringEnumMemberConverterOptions(deserializationFailureFallbackValue: Unknown)]
public enum ErrorCode
{
    Unknown,

    /// <summary>
    /// Must be authenticated as a server
    /// </summary>
    AccessDenied,
    
    /// <summary>
    /// An attachment is missing data
    /// </summary>
    AttachmentMissingData,
    
    /// <summary>
    /// An attachment is missing a name
    /// </summary>
    AttachmentMissingName,

    /// <summary>
    /// The From address is missing and is required
    /// </summary>
    FromAddressMissing,
    
    /// <summary>
    /// The API token provided in X-Server-API-Key was not valid
    /// </summary>
    InvalidServerAPIKey,
    
    /// <summary>
    /// No message found matching provided ID
    /// </summary>
    MessageNotFound,
    
    /// <summary>
    /// There is no content defined for this e-mail
    /// </summary>
    NoContent,
    
    /// <summary>
    /// There are no recipients defined to received this message
    /// </summary>
    NoRecipients,
    
    /// <summary>
    /// The mail server has been suspended
    /// </summary>
    ServerSuspended,
        
    /// <summary>
    /// API is currently unavailable for maintenance
    /// </summary>
    ServerUnavailable,
    
    /// <summary>
    /// The maximum number of BCC addresses has been reached (maximum 50)
    /// </summary>
    TooManyBCCAddresses,
    
    /// <summary>
    /// The maximum number of CC addresses has been reached (maximum 50)
    /// </summary>
    TooManyCCAddresses,
        
    /// <summary>
    /// The maximum number of To addresses has been reached (maximum 50)
    /// </summary>
    TooManyToAddresses,
    
    /// <summary>
    /// The From address is not authorised to send mail from this server
    /// </summary>
    UnauthenticatedFromAddress,
    
    /// <summary>
    /// The provided data was not sufficient to send an email
    /// </summary>
    ValidationError
}