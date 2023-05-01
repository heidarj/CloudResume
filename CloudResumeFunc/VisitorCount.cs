using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace CloudResumeFunc
{
	public static class VisitorCount
    {
        [FunctionName("VisitorCount")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "CloudResume",
                containerName: "Visitors",
                Connection = "CosmosDBConnection",
                SqlQuery = "SELECT count(c.id) as Count FROM c")]
                IEnumerable<CountResult> count,
            ILogger log)
        {
            return new OkObjectResult(count.First().Count);
        }
    }

    public class CountResult
    {
        public int Count { get; set; }
    }
}
