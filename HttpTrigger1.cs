using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Company.Function;

public class HttpTrigger1
{
    private readonly IConfiguration _configuration;

    public HttpTrigger1(IConfiguration configuration)
        => _configuration = configuration;

    [FunctionName("HttpTrigger1")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest request)
    {
        // Read configuration data
        const string key = "database1:host";
        var value = _configuration[key];

        // Return the requested data
        return string.IsNullOrWhiteSpace(value)
            ? new BadRequestObjectResult($"The key {key} was not found in App Config")
            : new OkObjectResult($"key:[{key}] - value:[{value}]");
    }
}
