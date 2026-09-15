using RDiazClock.Core.Services;
using Xunit;

namespace RDiazClock.Tests
{
    public class ClockServiceTests
    {
        private readonly ClockService _service = new();

        [Fact]
        public void GetSoundForTime_ShouldReturnTick_OnRegularSecond()
        {
            var time = new DateTime(2026, 1, 1, 10, 15, 23);
            var sound = _service.GetSoundForTime(time);
            Assert.Equal("tick", sound);
        }

        [Fact]
        public void GetSoundForTime_ShouldReturnTock_OnMinuteMark()
        {
            var time = new DateTime(2026, 1, 1, 10, 15, 0);
            var sound = _service.GetSoundForTime(time);
            Assert.Equal("tock", sound);
        }

        [Fact]
        public void GetSoundForTime_ShouldReturnBong_OnHourMark()
        {
            var time = new DateTime(2026, 1, 1, 10, 0, 0);
            var sound = _service.GetSoundForTime(time);
            Assert.Equal("bong", sound);
        }

        [Fact]
        public void UpdateSettings_ShouldReflectNewSoundValue()
        {
            _service.UpdateSettings(tick: "quack", tock: null, bong: null);

            var time = new DateTime(2026, 1, 1, 10, 15, 23);
            var sound = _service.GetSoundForTime(time);

            Assert.Equal("quack", sound);
        }
    }
}
