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
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Azure.Messaging.EventHubs;
using Dapr.AzureFunctions.Extension;
using CloudNative.CloudEvents;

namespace Athena
{
    public class TelemetriesFunction
    {
        private readonly ProcessDataService dataService;
        private readonly DeviceConfigurationService deviceConfigurationService;
        private readonly IDeviceForwarder deviceForwardService;
        private readonly IMapper mapper;
        private readonly ILogger<TelemetriesFunction> log;

        public TelemetriesFunction(
            ProcessDataService dataService,
            DeviceConfigurationService deviceConfigurationService,
            IDeviceForwarder deviceForwardService,
            IMapper mapper, ILogger<TelemetriesFunction> log)
        {
            this.dataService = dataService;
            this.deviceConfigurationService = deviceConfigurationService;
            this.deviceForwardService = deviceForwardService;
            this.mapper = mapper;
            this.log = log;
        }

        [FunctionName("TelemetriesHttp")]
        public async Task<IActionResult> RunHttp(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "telemetries")] HttpRequest req)
        {
            string requestBody = string.Empty;
            using (var sr = new StreamReader(req.Body))
            {
                requestBody = await sr.ReadToEndAsync();
            }
            TelemetriesSetDto payload = JsonConvert.DeserializeObject<TelemetriesSetDto>(requestBody);

            try
            {
                TelemetryDispatchDto data = Process(payload.DeviceId, payload);

                IActionResult result = data.Configuration.IsPublished ?
                    new StatusCodeResult((int)HttpStatusCode.NotModified) :
                    new OkObjectResult(new { publicationDelay = data.Configuration.PublicationDelay });

                return result;
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new
                {
                    Message = "Error while processing telemetries",
                    InnerMessage = e.Message
                });
            }
        }


        [FunctionName("TelemetriesEvent")]
        public async Task RunEvent(
            [EventHubTrigger("%EventSubscribe%", Connection = "EventSubscribeConnectionString")] EventData[] events,
            [EventHub("%EventPublish%", Connection = "EventPublishConnectionString")] IAsyncCollector<string> outputEvents)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    string requestBody = eventData.EventBody.ToString();
                    TelemetriesSetDto payload = JsonConvert.DeserializeObject<TelemetriesSetDto>(requestBody);

                    TelemetryDispatchDto data = Process(payload.DeviceId, payload);
                    await outputEvents.AddAsync(JsonConvert.SerializeObject(data));

                    await deviceForwardService.ForwardDeviceConfiguration(mapper.Map<DeviceConfiguration>(data?.Configuration), data?.Configuration.IsPublished ?? false);
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }

        [FunctionName("DaprEvent")]
        public async Task RunDapr(
            [DaprTopicTrigger(pubSubName: "%subComponentName%", Topic = "%EventSubscribe%")] CloudEvent cloudEvent,
            [DaprPublish(PubSubName = "%pubComponentName%", Topic = "%EventPublish%")] IAsyncCollector<DaprPubSubEvent> outputEvents)
        {
            TelemetriesSetDto payload = JsonConvert.DeserializeObject<TelemetriesSetDto>(cloudEvent.Data.ToString());

            try
            {
                TelemetryDispatchDto data = Process(payload.DeviceId, payload);

                await outputEvents.AddAsync(new DaprPubSubEvent(JsonConvert.SerializeObject(data)));

                await deviceForwardService.ForwardDeviceConfiguration(mapper.Map<DeviceConfiguration>(data?.Configuration), data?.Configuration.IsPublished ?? false);
            }
            catch (Exception e)
            {
                log.LogError(e, "An error occured on processing telemetries");
            }
        }


        private TelemetryDispatchDto Process(string deviceId, TelemetriesSetDto data)
        {
            try
            {
                IEnumerable<Telemetry> telemetries = mapper.Map<IEnumerable<Telemetry>>(data.Telemetries);
                Pool pool = this.dataService.Process(deviceId, telemetries);
                DeviceConfiguration configuration = deviceConfigurationService.GetDeviceConfiguration(deviceId);

                if (configuration != null && !deviceConfigurationService.SetAsPublished(configuration))
                {
                    log.LogWarning($"DeviceConfiguration {configuration.Id} is published but stay as 'unpublished' in the database");
                }

                log.LogInformation($"{DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm} - Telemetries updated for device {deviceId}");

                return mapper.Map<TelemetryDispatchDto>((pool, telemetries, configuration));
            }
            catch (Exception e)
            {
                log.LogError(e.Message, e);
                throw;
            }
        }
    }
}
