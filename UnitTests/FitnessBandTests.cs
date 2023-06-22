using System;
using NUnit.Framework;
using OOP_Lab6.WatchFuncs;

namespace UnitTestsLab_6
{
    [TestFixture]
     class FitnessBandTests
    {
        [Test]
        public void TestMeasureHeartRate()
        {
            // Arrange
            FitnessBand fitnessBand = new FitnessBand();

            // Act
            fitnessBand.MeasureHeartRate();

            // Assert
            Assert.GreaterOrEqual(fitnessBand.HeartRate, 60);
            Assert.LessOrEqual(fitnessBand.HeartRate, 180);
        }

        [Test]
        public void TestCheckHeartAttack()
        {
            // Arrange
            FitnessBand fitnessBand = new FitnessBand();
            fitnessBand.MeasureHeartRate();

            // Act & Assert
            Assert.DoesNotThrow(() => fitnessBand.CheckHeartAttack());
        }

        [Test]
        public void TestCheckHeartAttackHighRisk()
        {
            // Arrange
            FitnessBand fitnessBand = new FitnessBand();
            fitnessBand.MeasureHeartRate();
            fitnessBand.CheckHeartAttack();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => fitnessBand.CheckHeartAttack());
        }

        [Test]
        public void TestResetCounters()
        {
            // Arrange
            FitnessBand fitnessBand = new FitnessBand();
            fitnessBand.AddSteps();
            fitnessBand.MeasureHeartRate();

            // Act
            fitnessBand.ResetCounters();

            // Assert
            Assert.AreEqual(0, fitnessBand.CountOfSteps);
            Assert.AreEqual(0, fitnessBand.Calories);
            Assert.AreEqual(0, fitnessBand.Kilometers);
            Assert.AreEqual(0, fitnessBand.HeartRate);
        }

        [Test]
        public void TestRecordWorkout()
        {
            // Arrange
            FitnessBand fitnessBand = new FitnessBand();
            double distance = 5.0;
            int minutes = 30;

            // Act
            fitnessBand.RecordWorkout(minutes, distance);

            // Assert
            Assert.AreEqual(distance, fitnessBand.Kilometers);
            Assert.AreEqual(minutes * 7.5, fitnessBand.Calories);
        }

        [Test]
        public void TestRecordWorkoutEvent()
        {
            // Arrange
            FitnessBand fitnessBand = new FitnessBand();
            double distance = 5.0;
            int minutes = 30;

            // Act
            fitnessBand.AddWorkoutRecordedSubscriber(fitnessBand.HandleWorkoutRecorded);
            fitnessBand.RecordWorkout(minutes, distance);

            // Assert
            Assert.Pass("WorkoutRecorded event was successfully invoked");
        }
    }
}