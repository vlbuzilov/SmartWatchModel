using System;
using System.Threading;
using NUnit.Framework;
using OOP_Lab6.WatchFuncs;

namespace UnitTestsLab_6
{
    [TestFixture]
    public class SleepTrackerTests
    {
        [Test]
        public void StartSleepTracking_ShouldSetCurrentSleepData()
        {
            // Arrange
            var watch = new SleepTrackerWatch("TestWatch");

            // Act
            watch.StartSleepTracking();

            // Assert
            Assert.IsNotNull(watch.CurrentSleepData);
        }

        [Test]
        public void StopSleepTracking_ShouldAddSleepDataToList()
        {
            // Arrange
            var watch = new SleepTrackerWatch("TestWatch");
            watch.StartSleepTracking();

            // Act
            watch.StopSleepTracking();

            // Assert
            Assert.AreEqual(1, watch.SleepDataList.Count);
        }

        [Test]
        public void GetTotalSleepTime_ShouldReturnZeroIfNoSleepData()
        {
            // Arrange
            var watch = new SleepTrackerWatch("TestWatch");

            // Act
            var totalSleepTime = watch.GetTotalSleepTime();

            // Assert
            Assert.AreEqual(TimeSpan.Zero, totalSleepTime);
        }

        [Test]
        public void GetTotalSleepTime_ShouldReturnSleepTimeIfOneSleepData()
        {
            // Arrange
            var watch = new SleepTrackerWatch("TestWatch");
            watch.StartSleepTracking();
            watch.StopSleepTracking();

            // Act
            var totalSleepTime = watch.GetTotalSleepTime();

            // Assert
            Assert.AreEqual(watch.SleepDataList[0].GetSleepTime(), totalSleepTime);
        }

        [Test]
        public void GetTotalSleepTime_ShouldReturnSleepTimeIfMultipleSleepData()
        {
            // Arrange
            var watch = new SleepTrackerWatch("TestWatch");
            watch.StartSleepTracking();
            watch.StopSleepTracking();
            watch.StartSleepTracking();
            watch.StopSleepTracking();

            // Act
            var totalSleepTime = watch.GetTotalSleepTime();

            // Assert
            var expectedSleepTime = watch.SleepDataList[0].GetSleepTime() + watch.SleepDataList[1].GetSleepTime();
            Assert.AreEqual(expectedSleepTime, totalSleepTime);
        }
        
    }
}