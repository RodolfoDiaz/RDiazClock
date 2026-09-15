using RDiazClock.Core.Models;
using RDiazClock.Core.Services;

namespace RDiazClock.Core.Services;

public class ClockService : IClockService
{
    private readonly ClockSettings _settings = new();
    private readonly object _lock = new();

    public string GetSoundForTime(DateTime time)
    {
        lock (_lock)
        {
            // Hourly priority ("bong")
            if (time.Minute == 0 && time.Second == 0)
            {
                return _settings.BongMessage;
            }

            // Minutely priority ("tock")
            if (time.Second == 0)
            {
                return _settings.TockMessage;
            }

            // Second priority ("tick")
            return _settings.TickMessage;
        }
    }

    public void UpdateSettings(string? tick, string? tock, string? bong)
    {
        lock (_lock)
        {
            if (!string.IsNullOrWhiteSpace(tick)) _settings.TickMessage = tick;
            if (!string.IsNullOrWhiteSpace(tock)) _settings.TockMessage = tock;
            if (!string.IsNullOrWhiteSpace(bong)) _settings.BongMessage = bong;
        }
    }

    public ClockSettings GetSettings()
    {
        lock (_lock)
        {
            return new ClockSettings
            {
                TickMessage = _settings.TickMessage,
                TockMessage = _settings.TockMessage,
                BongMessage = _settings.BongMessage
            };
        }
    }
}