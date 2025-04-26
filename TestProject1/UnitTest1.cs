using Containers.Application;
using Containers.Models;
using Xunit;
using System;

public class DeviceServiceTests
{
    private readonly DeviceService _deviceService;

    public DeviceServiceTests()
    {
        // Example connection string (you should use test database or mock here)
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDatabase;Trusted_Connection=True;";
        _deviceService = new DeviceService(connectionString);
    }

    [Fact]
    public void AddEmbedded_WithInvalidIp_ThrowsException()
    {
        // Arrange
        var embedded = new Embedded
        {
            DeviceID = 1,
            NetworkName = "NetworkA",
            IpAddress = "InvalidIp"
        };

        // Act + Assert
        var ex = Assert.Throws<ArgumentException>(() => _deviceService.AddEmbedded(embedded));
        Assert.Equal("Invalid IP address format.", ex.Message);
    }

    [Fact]
    public void AddEmbedded_WithEmptyNetworkName_ThrowsException()
    {
        // Arrange
        var embedded = new Embedded
        {
            DeviceID = 1,
            NetworkName = "   ",  // whitespace
            IpAddress = "192.168.1.1"
        };

        // Act + Assert
        var ex = Assert.Throws<ArgumentException>(() => _deviceService.AddEmbedded(embedded));
        Assert.Equal("Network name cannot be empty or whitespace.", ex.Message);
    }

    [Fact]
    public void AddSmartWatch_WithInvalidBatteryPercentage_ThrowsException()
    {
        // Arrange
        var smartWatch = new SmartWatch
        {
            DeviceID = 1,
            BatteryPercentage = 150 // invalid
        };

        // Act + Assert
        var ex = Assert.Throws<ArgumentException>(() => _deviceService.AddSmartWatch(smartWatch));
        Assert.Equal("Invalid Battery Percentage.", ex.Message);
    }
    
}
