using Athena.Models;
using Athena.Dtos;
using System.Collections.Generic;
using System;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Athena.Services
{
    public class ProcessDataService
    {
        private readonly PoolService poolService;
        private readonly TelemetryService telemetryService;
        private readonly IMapper mapper;
        private readonly ILogger log;

        public ProcessDataService(PoolService poolService, TelemetryService telemetryService, IMapper mapper, ILogger log)
        {
            this.poolService = poolService;
            this.telemetryService = telemetryService;
            this.mapper = mapper;
            this.log = log;
        }

        public void Process(string deviceId, TelemetriesSetDto data)
        {
            if (!data.Telemetries.Any()) return;

            Pool pool = poolService.GetByDeviceId(deviceId);
            if (pool == null)
            {
                log.LogError($"No pool associated to device {deviceId}");
                throw new Exception($"Pool not found");
            }

            IEnumerable<Telemetry> telemetries = mapper.Map<IEnumerable<Telemetry>>(data.Telemetries);
            foreach(Telemetry telemetry in telemetries)
            {
                telemetry.Pool = pool;
                telemetry.DateTime = DateTimeOffset.UtcNow;
                telemetryService.Add(telemetry);
            }
        }
    }
}
