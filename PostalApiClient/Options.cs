namespace PostalApiClient;

/// <summary>
/// Configurations 
/// </summary>
public class Options
{
    /// <summary>
    /// Server URL
    /// </summary>
    public string Server { get; set; }
        
    /// <summary>
    /// Credential API key
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// Server DKIM Public key
    /// </summary>
    public string? DkimP { get; set; }
}