using OneOf;
using PostalApiClient.v1.Models;
using PostalApiClient.v1.Models.MessageInfos;
using PostalApiClient.v1.Sends.Models;

namespace PostalApiClient.v1;

public partial class PostalClient
{
    /// <summary>
    /// Sends a message. https://apiv1.postalserver.io/controllers/send/message.html
    /// </summary>
    /// <param name="from">The e-mail address for the From header</param>
    /// <param name="to">The e-mail addresses of the recipients (max 50)</param>
    /// <param name="cc">The e-mail addresses of any CC contacts (max 50)</param>
    /// <param name="bcc">The e-mail addresses of any BCC contacts (max 50)</param>
    /// <param name="sender">The e-mail address for the Sender header</param>
    /// <param name="subject">The subject of the e-mail</param>
    /// <param name="tag">The tag of the e-mail</param>
    /// <param name="replyTo">Set the reply-to address for the mail</param>
    /// <param name="plainBody">The plain text body of the e-mail</param>
    /// <param name="htmlBody">The HTML body of the e-mail</param>
    /// <param name="attachments">An array of attachments for this e-mail</param>
    /// <param name="headers">A hash of additional headers</param>
    /// <param name="bounce">Is this message a bounce?</param>
    /// <returns></returns>
    public async Task<OneOf<SendResponse, SendError>> SendMessageAsync(string from, List<string> to, List<string>? cc = null, List<string>? bcc = null,
        string? sender = null, string? subject = null, string? tag = null, string? replyTo = null, string? plainBody = null,
        string? htmlBody = null, List<PostalMessageAttachment>? attachments = null, IDictionary<string, string>? headers = null,
        bool bounce = false)
        => await SendMessageAsync(new PostalMessage
        {
            To = to,
            Cc = cc,
            Bcc = bcc,
            From = from,
            Sender = sender,
            Subject = subject,
            Tag = tag,
            ReplyTo = replyTo,
            PlainBody = plainBody,
            HtmlBody = htmlBody,
            Attachments = attachments,
            Headers = headers,
            Bounce = bounce
        });

    /// <summary>
    /// Sends a message. https://apiv1.postalserver.io/controllers/send/message.html
    /// </summary>
    /// <returns></returns>
    public async Task<OneOf<SendResponse, SendError>> SendMessageAsync(PostalMessage requestModel)
        => await ExecuteAsync<SendResponse, SendError>("send/message", requestModel);
    
    /// <summary>
    /// Send message from raw Base64 encoded RFC2822 formatted message. https://apiv1.postalserver.io/controllers/send/raw.html
    /// </summary>
    /// <param name="mailFrom">The address that should be logged as sending the message.</param>
    /// <param name="rcptTo">The addresses this message should be sent to.</param>
    /// <param name="data">The base64 encoded RFC2822 message to send.</param>
    /// <param name="bounce">Is this message a bounce?</param>
    /// <returns></returns>
    public async Task<OneOf<SendResponse, BaseError>> SendRawMessageAsync(string mailFrom, List<string> rcptTo, string data, bool? bounce = false)
        => await SendRawMessageAsync(new PostalRawMessage
        {
            MailFrom = mailFrom,
            RcptTo = rcptTo,
            Data = data,
            Bounce = bounce
        });
        
    public async Task<OneOf<SendResponse, BaseError>> SendRawMessageAsync(PostalRawMessage requestModel)
        => await ExecuteAsync<SendResponse, BaseError>("send/raw", requestModel);
}