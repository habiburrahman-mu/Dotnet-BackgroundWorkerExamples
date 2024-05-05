using Hangfire;

namespace HangfireExample.Jobs
{
    public interface ITestJob
    {
        Task RunAsync(int id, string type);
    }

    public class TestJob :ITestJob
    {
        [JobDisplayName($"{nameof(TestJob)}")]
        public async Task RunAsync(int id, string type)
        {
            Console.WriteLine($"Staring job {nameof(TestJob)} with type {type} ,value {id}");
            await Task.Delay(10000);
            Console.WriteLine($"Job {nameof(TestJob)} with type {type} done.");
        }
    }
}
