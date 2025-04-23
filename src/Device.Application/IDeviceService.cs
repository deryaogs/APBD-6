using Containers.Models;

namespace Containers.Application;

public interface IDeviceService
{
    IEnumerable<Devices> AllDevice();
    bool AddDevice(Devices devices);
    bool RemoveDevice(Devices devices);
    bool UpdateDevice(Devices devices);
    Devices? GetDeviceById(string id);
}