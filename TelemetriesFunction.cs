using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Athena.Dtos;
using Athena.Models;
using Athena.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Athena
{
    public class TelemetriesFunction
    {
        private readonly ProcessDataService dataService;
        private readonly DeviceConfigurationService deviceConfigurationService;

        public TelemetriesFunction(ProcessDataService dataService, DeviceConfigurationService deviceConfigurationService)
        {
            this.dataService = dataService;
            this.deviceConfigurationService = deviceConfigurationService;
        }

        [FunctionName("Telemetries")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Telemetries/{deviceId}")]HttpRequest req,
            string deviceId,
            ILogger log)
        {
            string requestBody = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                requestBody = await sr.ReadToEndAsync();
            }
            TelemetriesSetDto payload = JsonConvert.DeserializeObject<TelemetriesSetDto>(requestBody);

            try
            {
                dataService.Process(deviceId, payload);
                DeviceConfiguration configuration = deviceConfigurationService.GetDeviceConfiguration(deviceId);

                IActionResult result  = configuration.IsPublished && !req.Query.ContainsKey("getConfiguration") ? 
                    new StatusCodeResult((int)HttpStatusCode.NotModified) as IActionResult :
                    new OkObjectResult(new { publicationDelay = configuration.PublicationDelay.TotalSeconds });

                if(!deviceConfigurationService.SetAsPublished(configuration))
                {
                    log.LogWarning($"DeviceConfiguration {configuration.Id} is published but stay as 'unpublished' in the database");
                }

                log.LogInformation($"{DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm} - Telemetries updated for device {deviceId}");

                return result;
            }
            catch (Exception e)
            {
                log.LogError(e.Message, e);
                return new BadRequestObjectResult(new
                {
                    Message = "Error while processing telemetries",
                    InnerMessage = e.Message
                });
            }
        }
    }
}
