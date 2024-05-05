using Hangfire;

namespace HangfireExample.Jobs
{
    public interface ITestRecurringJob
    {
        Task RunAsync();
    }

    public class TestRecurringJob : ITestRecurringJob
    {
        [JobDisplayName(nameof(TestRecurringJob))]
        public async Task RunAsync()
        {
            Console.WriteLine($"Starting work on {nameof(TestRecurringJob)}");
            await Task.Delay(5000);
            Console.WriteLine($"Done from {nameof(TestRecurringJob)}");
        }
    }
}
