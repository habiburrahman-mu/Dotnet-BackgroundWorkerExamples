using Hangfire;
using HangfireExample.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace HangfireExample.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class HangfireTestController : ControllerBase
    {
        private readonly ITestJob testJob;

        public HangfireTestController(ITestJob testJob)
        {
            this.testJob = testJob;
        }

        [HttpGet]
        [Route("immediate/{id}")]
        public string Immediate(int id)
        {
            var jobId = BackgroundJob.Enqueue(() => testJob.RunAsync(id, "immediate"));
            return $"Job ID: {jobId}. Job started with value {id}";
        }

        [HttpGet]
        [Route("schedule/{id}")]
        public string Schedule(int id)
        {
            var jobId = BackgroundJob.Schedule(() => testJob.RunAsync(id, "schedule"), TimeSpan.FromSeconds(20));
            return $"Job ID: {jobId}. Job will start after 20 second";
        }
    }
}
