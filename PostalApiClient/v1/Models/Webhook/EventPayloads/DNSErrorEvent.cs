using System.Text.Json.Serialization;

namespace PostalApiClient.v1.Models.Webhook.EventPayloads;

public class DNSErrorEvent
{
    private DateTime? _dnsCheckedAtDateTime;
    
    [JsonPropertyName("domain")]
    public string Domain { get; set; }
    
    [JsonPropertyName("uuid")]
    public string Uuid { get; set; }
    
    [JsonPropertyName("dns_checked_at")]
    public double? DnsCheckedAt { get; set; }
    
    [JsonPropertyName("spf_status")]
    public DNSCheckStatus SpfStatus { get; set; }
    
    [JsonPropertyName("spf_error")]
    public string? SpfError { get; set; }
    
    [JsonPropertyName("dkim_status")]
    public DNSCheckStatus DkimStatus { get; set; }
    
    [JsonPropertyName("dkim_error")]
    public string? DkimError { get; set; }
    
    [JsonPropertyName("mx_status")]
    public DNSCheckStatus MxStatus { get; set; }
    
    [JsonPropertyName("mx_error")]
    public string? MxError { get; set; }
    
    [JsonPropertyName("return_path_status")]
    public DNSCheckStatus ReturnPathStatus { get; set; }
    
    [JsonPropertyName("return_path_error")]
    public string? ReturnPathError { get; set; }

    [JsonPropertyName("server")]
    public DnsCheckServer Server { get; set; }
    
    /// <summary>
    /// The timestamp of the payload converted to DateTime
    /// </summary>
    public DateTime? DnsCheckedAtDateTime
    {
        get
        {
            if (_dnsCheckedAtDateTime == null && DnsCheckedAt != null)
            {
                _dnsCheckedAtDateTime = DateTimeOffset.FromUnixTimeSeconds((long) DnsCheckedAt.Value).DateTime;
            }

            return _dnsCheckedAtDateTime;
        }
    }
}

/// <summary>
/// Postal mail server info
/// </summary>
public class DnsCheckServer
{
    [JsonPropertyName("uuid")]
    public string Uuid { get; set; }
    
    /// <summary>
    /// Server name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    /// <summary>
    /// Server short name
    /// </summary>
    [JsonPropertyName("permalink")]
    public string Permalink { get; set; }
    
    /// <summary>
    /// Organization name
    /// </summary>
    [JsonPropertyName("organization")]
    public string Organization { get; set; }
}

