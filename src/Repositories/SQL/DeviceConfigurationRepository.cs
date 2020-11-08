using System.Linq;
using Microsoft.EntityFrameworkCore;
using Athena.Configuration;
using Athena.Models;

namespace Athena.Repositories.SQL
{
    public class DeviceConfigurationRepository : IDeviceConfigurationRepository
    {
        private readonly AthenaContext context;

        public DeviceConfigurationRepository(AthenaContext context)
        {
            this.context = context;
        }

        public DeviceConfiguration Get(int configurationId)
        {
            return context.DeviceConfiguration.FirstOrDefault(e => e.Id.Equals(configurationId));
        }

        public DeviceConfiguration GetByDevice(string deviceId)
        {
            return context.Devices
                .Include(e => e.Configuration)
                .FirstOrDefault(e => e.DeviceId.Equals(deviceId))?.Configuration;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
