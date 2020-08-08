using Athena.Models;
using Athena.Repositories;

namespace Athena.Services
{
    public class DeviceConfigurationService
    {
        private readonly IDeviceConfigurationRepository deviceConfigurationRepository;

        public DeviceConfigurationService(IDeviceConfigurationRepository deviceConfigurationRepository)
        {
            this.deviceConfigurationRepository = deviceConfigurationRepository;
        }

        public DeviceConfiguration GetDeviceConfiguration(string deviceId)
        {
            return deviceConfigurationRepository.GetByDevice(deviceId);
        }

        public bool SetAsPublished(DeviceConfiguration configuration)
        {
            configuration.IsPublished = true;
            deviceConfigurationRepository.SaveChanges();

            return configuration.IsPublished;
        }
    }
}
