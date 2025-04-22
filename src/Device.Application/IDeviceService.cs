using Containers.Models;

namespace Containers.Application;

public interface IDeviceService
{
    IEnumerable<Device> AllDevice();
    bool AddDevice(Device device);
    bool RemoveDevice(Device device);
    bool UpdateDevice(Device device);
}