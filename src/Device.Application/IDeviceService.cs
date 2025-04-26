using Containers.Models;

namespace Containers.Application;

public interface IDeviceService
{
    IEnumerable<Devices> AllDevice();
    IEnumerable<Embedded> AllEmbedded();
    IEnumerable<PersonalComputer> AllPersonalComputer();
    IEnumerable<SmartWatch> AllSmartWatch();
    bool AddDevice(Devices devices);
    bool AddEmbedded(Embedded embedded);
    bool AddPersonalComputer(PersonalComputer personalComputer);
    bool AddSmartWatch(SmartWatch smartWatch);
    bool RemoveDevice(Devices devices);
    bool RemoveEmbedded(Embedded embedded);
    bool RemovePersonalComputer(PersonalComputer personalComputer);
    bool RemoveSmartWatch(SmartWatch smartWatch);
    bool UpdateDevice(Devices devices);
    bool UpdateEmbedded(Embedded embedded);
    bool UpdatePersonalComputer(PersonalComputer personalComputer);
    bool UpdateSmartWatch(SmartWatch smartWatch);
    Devices? GetDeviceById(string id);
}