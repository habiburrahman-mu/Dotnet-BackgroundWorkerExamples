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

