using System;
using System.Threading;

namespace IHostedServiceExample.Services
{
    public class SampleIHostedService : IHostedLifecycleService
    {
        public Task StartingAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Starting the SampleIHostedService");
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Start the SampleIHostedService");
            return Task.CompletedTask;
        }

        public Task StartedAsync(CancellationToken cancellationToken)
        {
            _ = DoSomeWork(cancellationToken);
            return Task.CompletedTask;
        }

        private async Task DoSomeWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(5000);
                Console.WriteLine($"From SampleIHostedService: Time is {DateTime.Now.ToLocalTime()}");
            }
        }

        public Task StoppingAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping the SampleIHostedService");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stop the SampleIHostedService");
            return Task.CompletedTask;
        }

        public Task StoppedAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopped the SampleIHostedService");
            return Task.CompletedTask;
        }
    }
}
