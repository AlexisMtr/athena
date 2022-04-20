using System;
using System.Threading.Tasks;
using Athena.Models;

namespace Athena.Services;
public interface IDeviceForwarder
{
    Task ForwardDeviceConfiguration(DeviceConfiguration deviceConfiguration, Func<DeviceConfiguration, bool> forward);
    Task ForwardDeviceConfiguration(DeviceConfiguration deviceConfiguration,bool forward = false);
}

public class EmptyDeviceForwarder : IDeviceForwarder
{
    public Task ForwardDeviceConfiguration(DeviceConfiguration deviceConfiguration, Func<DeviceConfiguration, bool> forward)
    {
        return Task.CompletedTask;
    }

    public Task ForwardDeviceConfiguration(DeviceConfiguration deviceConfiguration, bool forward = false)
    {
        return ForwardDeviceConfiguration(deviceConfiguration, c => forward);
    }
}