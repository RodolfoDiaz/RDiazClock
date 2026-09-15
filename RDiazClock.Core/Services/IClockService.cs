using RDiazClock.Core.Models;

namespace RDiazClock.Core.Services;

public interface IClockService
{
    string GetSoundForTime(DateTime time);
    void UpdateSettings(string? tick, string? tock, string? bong);
    ClockSettings GetSettings();
}