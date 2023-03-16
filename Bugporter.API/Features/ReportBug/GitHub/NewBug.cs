using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugporter.API.Features.ReportBug.GitHub
{
    public class NewBug
    {
        public string BugSummary { get; }
        public string Description { get; }

        public NewBug(string bugSummary, string description)
        {
            BugSummary = bugSummary;
            Description = description;
        }
    }
}
