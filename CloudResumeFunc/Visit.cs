using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using CloudResumeFunc.Models;
using MyCSharp.HttpUserAgentParser;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace CloudResumeFunc;

public static class VisitFunc
{
    [FunctionName("Visit")]
    public static IActionResult TableOutput(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
        [CosmosDB(
            databaseName: "CloudResume",
            containerName: "Visitors",
            Connection = "CosmosDBConnection")] out Visit visit,
        ILogger log)
    {
        var clientIP = (req.Headers["X-Forwarded-For"].FirstOrDefault() ?? "").Split(':').FirstOrDefault() ?? string.Empty;
        HttpUserAgentInformation info = HttpUserAgentParser.Parse(req.Headers["User-Agent"]);

        using var md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(clientIP + info.Name);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        var hashString = Convert.ToHexString(hashBytes);

        visit = new Visit
        {
            id = Guid.NewGuid().ToString(),
            IP = clientIP,
            UserAgent = req.Headers["User-Agent"].FirstOrDefault() ?? "",
            Platform = info.Platform?.Name,
            Browser = info.Name,
            Thumbprint = hashString
        };

        return new OkResult();
    }
}
