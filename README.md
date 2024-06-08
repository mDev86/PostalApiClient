<img src="./logo.png" width="50" height="50"> 

## PostalApiClient  

.NET Core API client for [postal](https://docs.postalserver.io/) mail delivery platform

## Available on NuGet
[![Version](https://img.shields.io/nuget/v/PostalApiClient)](https://www.nuget.org/packages/PostalApiClient)

## Usage

### 1. Register PostalClient in startup\builder
```csharp
// Read configuration from default section "PostalClient"
builder.Services
    .AddPostalApiClient(builder.Configuration);

// or set options from action
builder.Services
    .AddPostalApiClient(opt =>
    {
        opt.Server = "http://mypostal.domain";
        opt.ApiKey = "Api-Credential-Key";
    });

// or read options from custom configuration section
builder.Services
    .AddPostalApiClient(builder.Configuration.GetSection("MyCustomSection"));

// or use combination from custom configuration section
// and action for override some options
builder.Services
    .AddPostalApiClient(builder.Configuration.GetSection("MyCustomSection"),
        options => { 
            options.ApiKey = "OtherApiKey"; 
        });

// Can use built-in extension methods for setting HttpClient
// e.g. add request handler or logger or retry policy and etc.
builder.Services
    .AddPostalApiClient(builder.Configuration)
    .AddLogger<CustomHttpLogger>()
    .AddHttpMessageHandler<MyRequestHandler>();
```

### 2. Get client from DI
```csharp
// e.g. in controller
public class PostalController : ControllerBase
{
    private readonly PostalClient _postalClient;
    public PostalController(PostalClient client)
    {
        _postalClient = client;
    }
    
    // ... actions
}
```

### 3. Call API methods
```csharp
var result = await _postalClient.GetMessageDeliveriesAsync(messageId); 

if (result.IsT1)
{
    // error handler
}
// continue code with success result

return result.Match<IActionResult>(
    response => Ok(response),
    error => BadRequest(error));

```
All methods always return type `OneOf<TSuccess, TError>`. 
Your can check `result.IsT0` for make sure the operation is successful or `result.IsT1` that operation error.

[More details about OneOf](https://github.com/mcintyre321/OneOf)

Samples
```csharp
// Send message
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

// Get message details
await _postalClient.GetMessageDetailsAsync(messageId, MessageExpansion.Status | MessageExpansion.PlainBody);
```

All available methods and description see in [official docs](https://apiv1.postalserver.io/) or samples in Demo project

### 4. Webhooks

#### Simple usage
Create webhook in postal server and add `POST`method in controller
```csharp
[HttpPost]
public IActionResult ReceiveWebhook([FromBody] PostalWebhook payload)
{
    // ... 
    // Your webhook handler code
    
    return Ok();
}
```
`PostalWebhook` support payload for all events

#### Signature verification
You're can verification request signature for approval that request was sent by your postal server.

For this need get public key from server DKIM record and set option `DkimP`
```shell
# run on postal server machine 
# and copy the p=... part of the TXT record (without the semicolon at the end)
postal default-dkim-record
```
```csharp
// Add public key to ApiClient option or in appsettings.json
builder.Services
    .AddPostalApiClient(opt =>
    {
        opt.DkimP = "p= part from drim TXT record";
    });
```
After can use service `PostalWebhookVerifier` for validate model `PostalWebhook ` signature
```csharp
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
```

You're can install package [PostalApiClient.Mvc.Extensions](https://www.nuget.org/packages/PostalApiClient.Mvc.Extensions) with mvc dependencies and use attribute for request signature automatic validation
```csharp
[HttpPost]
[PostalSignatureVerify]
public IActionResult ReceiveWebhookWithSignatureVerify([FromBody] PostalWebhook payload)
{
    // ... 
    // Your webhook handler code

    return Ok();
}
```
or write custom middleware for verification with use extension method
```csharp
// some code before verification

var isVerified = await context.HttpContext.CheckPostalSignatureAsync();

// some code after verification
```