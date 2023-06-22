using System;
using NUnit.Framework;
using OOP_Lab6.WatchFuncs;

namespace UnitTestsLab_6
{
    [TestFixture]
    public class ClockTests
    {
        private Clock clock;
        
        [SetUp]
        public void Setup()
        {
            clock = new Clock();
        }
        
        [Test]
        public void ResetStopWatch_ShouldResetStopWatch()
        {
            clock.StartStopWatch();
            clock.ResetStopWatch();
            Assert.AreEqual(clock.Stopwatch.Elapsed, TimeSpan.Zero);
        }

        [Test] public void SetAlarm_ShouldSetAlarm()
        {
            int hours = 22;
            int minutes = 30;
            int seconds = 0;

            clock.SetAlarm(hours, minutes, seconds);
            Assert.AreEqual(clock.Alarm, DateTime.Today.Add(new TimeSpan(hours, minutes, seconds)));
        }

        [Test]
        public void CheckAlarm_WithAlarmTimeNotPassed_ShouldNotTriggerAlarm()
        {
            int hours = DateTime.Now.Hour;
            int minutes = DateTime.Now.Minute + 1;
            int seconds = DateTime.Now.Second + 1;
            clock.SetAlarm(hours, minutes, seconds);

            bool result = clock.CheckAlarm();

            Assert.IsFalse(result);
        }

    }
}
