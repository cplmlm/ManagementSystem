using System;
using System.Threading;
using System.Threading.Tasks;
using ManagementSystem.Common;
using ManagementSystem.Common.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ManagementSystem.Extensions.HostedService;

public sealed class SeedDataHostedService : IHostedService
{
    private readonly MyContext _myContext;
    private readonly ILogger<SeedDataHostedService> _logger;
    private readonly string _webRootPath;

    public SeedDataHostedService(
        MyContext myContext,
        IHostEnvironment webHostEnvironment,
        ILogger<SeedDataHostedService> logger)
    {
        _myContext = myContext;
        _logger = logger;
        _webRootPath = webHostEnvironment.ContentRootPath;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start Initialization Db Seed Service!");
        await DoWork();
    }

    private async Task DoWork()
    {
        try
        {
            if (AppSettings.app("AppSettings", "SeedDBEnabled").ObjToBool() || AppSettings.app("AppSettings", "SeedDBDataEnabled").ObjToBool())
            {
                await DBSeed.SeedAsync(_myContext, _webRootPath);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured seeding the Database.");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stop Initialization Db Seed Service!");
        return Task.CompletedTask;
    }
}