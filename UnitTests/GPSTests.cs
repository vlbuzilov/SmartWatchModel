using System;
using NUnit.Framework;
using OOP_Lab6.WatchFuncs;

namespace UnitTestsLab_6
{
    [TestFixture]
    public class GPSTests
    {
        private GPSTracker gpsTracker;

    [SetUp]
    public void Setup()
    {
        gpsTracker = new GPSTracker(40.7128, -74.0060);
    }

    [Test]
    public void GetLastCoordinates_DefaultConstructor_ReturnsDefaultCoordinates()
    {
        // Arrange
        var gpsTracker = new GPSTracker();

        // Act
        string result = gpsTracker.GetLastCoordinates();

        // Assert
        Assert.AreEqual("Latitude: 0, Longitude: 0", result);
    }

    [Test]
    public void GetDistance_TwoCoordinates_ReturnsExpectedDistance()
    {
        // Arrange
        double lat2 = 41.8781;
        double lon2 = -87.6298;

        // Act
        double distance = gpsTracker.GetDistance(lat2, lon2);

        // Assert
        Assert.AreEqual(1257.538, distance, 0.001);
    }

    [Test]
    public void GetETA_TwoCoordinatesAndSpeed_ReturnsExpectedDateTime()
    {
        // Arrange
        double lat2 = 41.8781;
        double lon2 = -87.6298;
        double speed = 60;

        // Act
        DateTime dateTime = gpsTracker.GetETA(lat2, lon2, speed);

        // Assert
        Assert.AreEqual(DateTime.Today.AddDays(1), dateTime.Date);
        Assert.AreEqual(1, dateTime.Hour);
    }
    
    [Test]
    public void IsWithinRadius_TwoCoordinatesAndRadius_ReturnsExpectedResult()
    {
        // Arrange
        double lat2 = 41.8781;
        double lon2 = -87.6298;
        double radius = 2000;

        // Act
        bool result = gpsTracker.IsWithinRadius(lat2, lon2, radius);

        // Assert
        Assert.IsTrue(result);
    }
    }
}