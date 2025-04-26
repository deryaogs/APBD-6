using Containers.Application;
using Containers.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("UniversityDatabase");
builder.Services.AddSingleton<IDeviceService, DeviceService>(deviceService => new DeviceService(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/devices/ShowAllDevice", (IDeviceService deviceService) =>
{
    try
    {
        return Results.Ok(deviceService.AllDevice());
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("/api/devices/ShowAllEmbedded", (IDeviceService deviceService) =>
{
    try
    {
        return Results.Ok(deviceService.AllEmbedded());
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("/api/devices/ShowAllSmartWatch", (IDeviceService deviceService) =>
{
    try
    {
        return Results.Ok(deviceService.AllSmartWatch());
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("/api/devices/ShowAllPersonalComputer", (IDeviceService deviceService) =>
{
    try
    {
        return Results.Ok(deviceService.AllPersonalComputer());
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("api/containers/AddDevice", (IDeviceService deviceService, Devices container) =>
{
    try
    {
        var result = deviceService.AddDevice(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});


app.MapPost("api/containers/AddEmbedded", (IDeviceService deviceService, Embedded container) =>
{
    try
    {
        var result = deviceService.AddEmbedded(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/containers/AddSmartWatch", (IDeviceService deviceService, SmartWatch container) =>
{
    try
    {
        var result = deviceService.AddSmartWatch(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/containers/AddPersonalComputer", (IDeviceService deviceService, PersonalComputer container) =>
{
    try
    {
        var result = deviceService.AddPersonalComputer(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/containers/RemoveDevice", (IDeviceService deviceService, Devices container) =>
{
    try
    {
        var result = deviceService.RemoveDevice(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/containers/RemoveEmbedded", (IDeviceService deviceService, Embedded container) =>
{
    try
    {
        var result = deviceService.RemoveEmbedded(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/containers/RemoveSmartWatch", (IDeviceService deviceService, SmartWatch container) =>
{
    try
    {
        var result = deviceService.RemoveSmartWatch(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPost("api/containers/RemovePeresonalComputer", (IDeviceService deviceService, PersonalComputer container) =>
{
    try
    {
        var result = deviceService.RemovePersonalComputer(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPut("api/containers/UpdateDevice", (IDeviceService deviceService, Devices container) =>
{
    try
    {
        var result = deviceService.UpdateDevice(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPut("api/containers/UpdateSmartWatch", (IDeviceService deviceService, SmartWatch container) =>
{
    try
    {
        var result = deviceService.UpdateSmartWatch(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPut("api/containers/UpdateEmbedded", (IDeviceService deviceService, Embedded container) =>
{
    try
    {
        var result = deviceService.UpdateEmbedded(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapPut("api/containers/UpdatePersonalComputer", (IDeviceService deviceService, PersonalComputer container) =>
{
    try
    {
        var result = deviceService.UpdatePersonalComputer(container);
        if (result is true)
        {
            return Results.Created("/api/containers", result);
        }
        else
        {
            return Results.BadRequest();
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});
app.MapGet("/api/containers/ShowDeviceByID", (IDeviceService deviceService, string id) =>
{
    try
    {
        var device = deviceService.GetDeviceById(id);
        if (device is not null)
        {
            return Results.Ok(device);
        }
        else
        {
            return Results.NotFound($"Device with ID '{id}' not found.");
        }
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();
