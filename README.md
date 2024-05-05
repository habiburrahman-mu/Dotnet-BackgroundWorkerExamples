# BackgroundWorkerExample

## IHostedService, IHostedLifecycleService
While `IHostedService` provides a solid foundation for background tasks in ASP.NET Core, .NET 8 introduces `IHostedLifecycleService` to offer even more granular control over the application lifecycle. This interface inherits from `IHostedService` and adds additional methods for specific lifecycle events.

#### Functionality of IHostedLifecycleService
- Methods
	- Inherits `StartAsync(CancellationToken)` and `StopAsync(CancellationToken)` from `IHostedService`.
    - `Starting(CancellationToken cancellationToken)`: This method is called just ater `StartAsync` is invoked. It allows for any post-startup actions.
    - `StartedAsync(CancellationToken cancellationToken)`: This method is called just before `StartAsync` is invoked. It allows for any pre-startup actions before the actual background task begins.
    - `StoppingAsync(CancellationToken cancellationToken)`: This method is called just before `StopAsync` is invoked. It allows for any pre-shutdown actions before the background task is stopped.
    - `StoppedAsync()`: This method is called after `StopAsync` has completed. You can use it for any post-shutdown cleanup or logging.

### Implementations
Implementation can be found in [HostedBackgroundService](HostedBackgroundService).

[`SampleIHostedService.cs`](HostedBackgroundService/Services/SampleIHostedService.cs) file holds the actual implementation of the background tasks. This service is registered in [`Program.cs`](HostedBackgroundService/Program.cs) through
```
builder.Services.AddHostedService<SampleIHostedService>();
```

- - -

## BackgroundService
`BackgroundService` is a concrete class provided by ASP.NET Core that inherits from the `IHostedService` interface. It offers a simplified approach to creating long-running background tasks within your application.

While `IHostedService` defines the core functionality for background services, `BackgroundService` provides a more convenient way to implement it. It reduces boilerplate code by handling some aspects of the lifecycle management automatically.

#### Functionality
- Inherits from `IHostedService`: `BackgroundService` inherits all the functionality of `IHostedService`, including the `StartAsync(CancellationToken cancellationToken)` and `StopAsync(CancellationToken cancellationToken)` methods.
- `ExecuteAsync(CancellationToken cancellationToken)`: This abstract method serves as the entry point for your background service's logic. You override this method to define the actual work your background task performs. The cancellation token allows for graceful termination during application shutdown.

### Implementation
Implementation can be found in [BackgroundService](BackgroundService).

[`SampleBackgroundService.cs`](BackgroundService/Services/SampleBackgroundService.cs) file holds the actual implementation of the background tasks. This service is registered in [`Program.cs`](BackgroundService/Program.cs) through
```
builder.Services.AddHostedService<SampleBackgroundService>();
```
`ExecuteAsync()` is overridden to implement the background task logic. It continuously loops until the cancellation token is signaled, performing work within the loop and simulating a delay between iterations.
- - -

## WorkerService

#### Creating a Worker Service Project:
ASP.NET Core provides a convenient Worker Service template to kickstart your project quickly. You can use the .NET CLI or Visual Studio to access this template.
```
dotnet new worker -o my-custom-worker
```

### Hosting Worker Service
Unlike traditional console applications or ASP.NET Core web apps, Worker Services themselves do not dictate how they should be hosted. This flexibility allows you to choose the hosting method that best aligns with your application's requirements.
**Here are some common hosting options:**
- **Scheduler-Triggered Console Application:**
	- Use tools like Windows Scheduled Tasks, Kubernetes cron jobs, Azure Logic Apps, AWS Scheduled Tasks, or GCP Cloud Scheduler to periodically execute a console application that starts your Worker Service.
- **Windows Service or systemd (Windows/Linux):**
	- For dedicated background task environments, consider creating a Windows Service (for Windows) or using systemd (on Linux) to host and manage your Worker Service as a long-running process.

### When to use worker services?
- **Out-of-Process Background Tasks:** Worker Services are ideal when you want your background tasks to run independently of a web application, providing greater isolation and resilience.
- **Prefer External Hosting: **If you prefer deploying your background tasks separately from your web application (e.g., containerized microservices), Worker Services offer a suitable solution.
- **Avoid App Pool Recycles:** Worker Services, when hosted externally, can circumvent web application pool recycles, ensuring uninterrupted background task execution.
- **Natural Migration Path:** If you're migrating existing full .NET Framework Windows Services to ASP.NET Core, Worker Services provide a natural transition path with a familiar background task management approach.

### Implementation
Implementation can be found in [WorkerServiceExample](WorkerServiceExample).

[`Worker.cs`](WorkerServiceExample/Worker.cs) file holds the actual implementation of the background tasks. This service is registered in [`Program.cs`](WorkerServiceExample/Program.cs) through
```
var builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .UseSystemd()
    .ConfigureServices((_, services) =>
    {
        services.AddHostedService<Worker>();
    });
```