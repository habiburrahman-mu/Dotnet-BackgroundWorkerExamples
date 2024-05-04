using BackgroundServiceExample.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<SampleBackgroundService>();

var app = builder.Build();

app.Run();