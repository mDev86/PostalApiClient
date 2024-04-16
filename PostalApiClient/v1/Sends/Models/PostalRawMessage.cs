namespace PostalApiClient.v1.Sends.Models;

public class PostalRawMessage
{
    public string MailFrom { get; set; }

    public ICollection<string> RcptTo { get; set; }

    public string Data { get; set; }

    public bool? Bounce { get; set; }
}