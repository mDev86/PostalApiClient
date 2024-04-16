<img src="./logo.png" width="50" height="50"> 

## PostalApiClient  

---

.NET Core API client for [postal](https://docs.postalserver.io/) mail delivery platform

## Available on NuGet
[![Version](https://img.shields.io/nuget/v/PostalApiClient)](https://www.nuget.org/packages/PostalApiClient)

### Usage

#### 1. Register PostalClient in startup\builder
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

#### 2. Get client from DI
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

#### 3. Call API methods
```csharp
var (result, error) = await _postalClient.GetMessageDeliveriesAsync(messageId); 

if (error != null)
{
    // error handler
}
// continue code with success result
```
All methods return tuples with two nullable items: `Result` and `Error`. 
Always check `Error`  for make sure the operation is successful.

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

var (result, error) = await _postalClient.SendMessageAsync(message); 

// Get message details
await _postalClient.GetMessageDetailsAsync(messageId, MessageExpansion.Status | MessageExpansion.PlainBody);
```

All available methods and description see in [official docs](https://apiv1.postalserver.io/) or samples in Demo project

#### 4. Webhooks

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