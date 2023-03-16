using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugporter.API.Features.ReportBug.GitHub
{
    public class CreateGitHubIssueCommand
    {
        private readonly ILogger<CreateGitHubIssueCommand> _logger;

        public CreateGitHubIssueCommand(ILogger<CreateGitHubIssueCommand> logger)
        {
            _logger = logger;
        }

        public async Task<ReportedBug> Execute(NewBug newBug)
        {
            _logger.LogInformation("Creating GitHub issue");
            var stopwatch = Stopwatch.StartNew(); // allocated on the heap :(

            // create GitHub issue
            ReportedBug reportedBug = new("1", "Test bug", "dummy bug description");


            stopwatch.Stop();
            _logger.LogInformation($"Creating GitHub issue {reportedBug.Id}, time elapsed {stopwatch.ElapsedMilliseconds}ms.");
            return reportedBug; 
        }
    }
}
