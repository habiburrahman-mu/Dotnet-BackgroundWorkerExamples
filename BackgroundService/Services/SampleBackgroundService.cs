
namespace BackgroundServiceExample.Services
{
    public class SampleBackgroundService : BackgroundService
    {
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"From SampleBackgroundService: Starting");
            base.StartAsync(cancellationToken);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000);
                Console.WriteLine($"From SampleBackgroundService: Time is {DateTime.Now.ToLocalTime()}");
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"From SampleBackgroundService: Stopping");
            base.StopAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
