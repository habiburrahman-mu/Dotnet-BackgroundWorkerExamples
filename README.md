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
[Test](BackgroundService)