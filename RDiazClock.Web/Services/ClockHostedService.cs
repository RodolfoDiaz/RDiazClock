using RDiazClock.Core.Services;
using RDiazClock.Web.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace RDiazClock.Web.Services;

public class ClockHostedService : BackgroundService
{
    private readonly IClockService _clockService;
    private readonly IHubContext<ClockHub> _hubContext;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly TimeSpan _duration = TimeSpan.FromHours(3);

    public ClockHostedService(
        IClockService clockService,
        IHubContext<ClockHub> hubContext,
        IHostApplicationLifetime appLifetime)
    {
        _clockService = clockService;
        _hubContext = hubContext;
        _appLifetime = appLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var startTime = DateTime.UtcNow;

        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            var now = DateTime.Now;

            // Stop execution after 3 hours
            if (DateTime.UtcNow - startTime >= _duration)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveLog", "3-hour runtime reached. Application shutting down...", stoppingToken);
                _appLifetime.StopApplication();
                break;
            }

            var message = _clockService.GetSoundForTime(now);
            await _hubContext.Clients.All.SendAsync("ReceiveTick", message, now.ToString("HH:mm:ss"), stoppingToken);
        }
    }
}