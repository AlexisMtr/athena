using Athena.Models;

namespace Athena.Repositories
{
    public interface IDeviceConfigurationRepository
    {
        DeviceConfiguration GetByDevice(string deviceId);
        void SaveChanges();
    }
}
