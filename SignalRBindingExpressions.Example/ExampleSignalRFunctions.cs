using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;

namespace SignalRBindingExpressions.Example
{
    public class ExampleSignalRFunctions
    {
        [FunctionName("ExampleSignalRFunctions_Negotiate")]
        public static Task<IActionResult> Negotiate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "negotiate")] 
            HttpRequest req,
            [SignalRConnectionInfo(HubName = "%SignalRHubName%", ConnectionStringSetting = "SignalRConnectionString", UserId = "Test_User")] 
            SignalRConnectionInfo connection,
            ILogger log)
        {
            log.LogInformation("Negotiate function called.");

            return Task.FromResult<IActionResult>(new OkObjectResult(connection));
        }

        [FunctionName("ExampleSignalRFunctions_OnConnected_ExpressionBinding")]
        public Task OnConnected_ExpressionBinding(
            [SignalRTrigger("%SignalRHubName%", "connections", "connected", ConnectionStringSetting = "SignalRConnectionString")]
            InvocationContext invocationContext,
            ILogger log)
        {
            log.LogInformation($"(ExpressionBinding) Connected: {invocationContext.UserId}");

            return Task.CompletedTask;
        }

        [FunctionName("ExampleSignalRFunctions_OnConnected_NoExpressionBinding")]
        public Task OnConnected_NoExpressionBinding(
            [SignalRTrigger("test_hub", "connections", "connected", ConnectionStringSetting = "SignalRConnectionString")]
            InvocationContext invocationContext,
            ILogger log)
        {
            log.LogInformation($"(NoExpressionBinding) Connected: {invocationContext.UserId}");

            return Task.CompletedTask;
        }
    }
}
