using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octokit;
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
        private readonly GitHubRepositoryOptions _gitHubRepositoryOptions;
        private readonly GitHubClient _gitHubClient;
        private readonly ILogger<CreateGitHubIssueCommand> _logger;

        public CreateGitHubIssueCommand(
            ILogger<CreateGitHubIssueCommand> logger, 
            GitHubClient gitHubClient,
            IOptions<GitHubRepositoryOptions> gitHubRepositoryOptions)
        {
            _logger = logger;
            _gitHubClient = gitHubClient;
            _gitHubRepositoryOptions = gitHubRepositoryOptions.Value;
        }

        public async Task<ReportedBug> Execute(NewBug newBug)
        {
            _logger.LogInformation("Creating GitHub issue");
            var stopwatch = Stopwatch.StartNew(); // allocated on the heap :(

            // create GitHub issue
            NewIssue newIssue = new(newBug.Summary)
            {
                Body = newBug.Description
            };

            Issue createdIssue = await _gitHubClient.Issue.Create(
                _gitHubRepositoryOptions.Owner,
                _gitHubRepositoryOptions.Name,
                newIssue);

            stopwatch.Stop();
            _logger.LogInformation($"Creating GitHub issue [{createdIssue.Number}] '{createdIssue.Title}', " 
                + $"time elapsed {stopwatch.ElapsedMilliseconds}ms.");
            return new ReportedBug(
                createdIssue.Number.ToString(), 
                createdIssue.Title, 
                createdIssue.Body); 
        }
    }
}
