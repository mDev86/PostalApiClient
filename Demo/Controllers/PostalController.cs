using Microsoft.AspNetCore.Mvc;
using PostalApiClient.Mvc.Extensions;
using PostalApiClient.Utilities;
using PostalApiClient.v1;
using PostalApiClient.v1.Messages.Models;
using PostalApiClient.v1.Models.Webhook;
using PostalApiClient.v1.Sends.Models;

namespace Demo.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PostalController : ControllerBase
{
    private readonly PostalClient _postalClient;
    public PostalController(PostalClient client)
    {
        _postalClient = client;
    }

    [HttpPost]
    public async Task<IActionResult> Send()
    {
        var message = new PostalMessage()
        {
            To = new List<string>
            {
                "example@example.com",
            },
            From = "admin@localhost.com",
            Subject = "Subject",
            PlainBody = "Message body text",
            Sender = "Sender email/name",
            Tag = "Custom message tag",
            ReplyTo = "replyTo@example.com",
            Attachments = new List<PostalMessageAttachment>
            {
                new PostalMessageAttachment()
                {
                    Data = "ContentBse64string",
                    Name = "Attachment №1",
                    ContentType = "image/jpeg"
                }
            },
            Headers = new Dictionary<string, string>
            {
                {"CustomMessageHeader","HeaderValue"}
            }
        };

        var result = await _postalClient.SendMessageAsync(message);

        return result.Match<IActionResult>(
            response => Ok(response),
            error => BadRequest(error)
            );
    }

    [HttpGet]
    public async Task<IActionResult> GetMessageDetails(int messageId, bool? allExpansions)
    {
        var result = allExpansions == true
            ? await _postalClient.GetMessageDetailsAsync(messageId, MessageExpansion.All)
            : await _postalClient.GetMessageDetailsAsync(messageId);

        return result.Match<IActionResult>(
            response => Ok(response),
            error => BadRequest(error)
            );
    }

    [HttpGet]
    public async Task<IActionResult> GetMessageDetailsWithSomeExpansions(int messageId)
    {
        var result = await _postalClient.GetMessageDetailsAsync(messageId,
            MessageExpansion.Status | MessageExpansion.PlainBody);

        return result.Match<IActionResult>(
            response => Ok(response),
            error => BadRequest(error)
            );
    }

    [HttpGet]
    public async Task<IActionResult> GetMessageDeliveries(int messageId)
    {
        var result = await _postalClient.GetMessageDeliveriesAsync(messageId);

        if (result.IsT1)
        {
            // error handler
        }

        return result.Match<IActionResult>(
            response => Ok(response),
            error => BadRequest(error)
            );
    }

    [HttpPost]
    public async Task<IActionResult> ReceiveWebhook([FromBody] PostalWebhook payload,
        [FromServices] PostalWebhookVerifier signatureVerifier)
    {
        // Check signature
        var isVerified = signatureVerifier.IsSignatureVerified(payload, Request.Headers);

        // !!IMPORTANT!! not use this method after request body already read.
        // This Request.Body usually is empty and verifier return always false
        var badUse = await signatureVerifier.IsSignatureVerifiedAsync(Request);

        // Your webhook handler code
        /// ...

        return Ok();
    }

    [HttpPost]
    [PostalSignatureVerify]
    public IActionResult ReceiveWebhookWithSignatureVerify([FromBody] PostalWebhook payload)
    {
        // ... 
        // Your webhook handler code

        return Ok();
    }
}