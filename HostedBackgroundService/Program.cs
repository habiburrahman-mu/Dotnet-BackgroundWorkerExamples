using IHostedServiceExample.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<SampleIHostedService>();

var app = builder.Build();
app.Run();
