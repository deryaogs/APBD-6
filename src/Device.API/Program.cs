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

app.MapGet("/api/containers", (IDeviceService deviceService) =>
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

app.MapPost("api/containers", (IDeviceService deviceService, Devices container) =>
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
app.MapPut("api/containers/", (IDeviceService deviceService, Devices container) =>
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

app.Run();
