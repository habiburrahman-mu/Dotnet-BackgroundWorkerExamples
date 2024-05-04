using WorkerServiceExample;

//var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddHostedService<Worker>();

//var host = builder.Build();
//host.Run();

var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .UseSystemd()
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();