using PostalApiClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add PostalClient
// and read configuration from default section "PostalClient"
builder.Services
    .AddPostalApiClient(builder.Configuration);

// or set options from action
/*builder.Services
    .AddPostalApiClient(opt =>
    {
        opt.Server = "http://mypostal.domain";
        opt.ApiKey = "Api-Credential-Key";
    });

// or read options from custom configuration section
builder.Services
    .AddPostalApiClient(builder.Configuration.GetSection("MyCustomSection"));

// or use combination from custom section and action for override some options
builder.Services
    .AddPostalApiClient(builder.Configuration.GetSection("MyCustomSection"),
        options => { options.ApiKey = "OtherApiKey"; });

// Can use built-in extension methods for setting HttpClient
// e.g. add request handler or logger or retry policy and etc.
builder.Services
    .AddPostalApiClient(builder.Configuration)
    .AddLogger<CustomHttpLogger>()
    .AddHttpMessageHandler<MyRequestHandler>();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();