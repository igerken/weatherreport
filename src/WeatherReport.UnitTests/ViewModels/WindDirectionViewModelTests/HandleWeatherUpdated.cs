using System;
using System.Threading;
using WeatherReport.WinApp.Events;
using WeatherReport.WinApp.ViewModels;
using Xunit;

namespace WeatherReport.UnitTests.ViewModels.WindDirectionViewModelTests;

public class HandleWeatherUpdated
{
    private const double DummyTemperature = 42.0;

    [Theory, AutoFakeData]
    public void Given_WindFromSouthWest__Then_ArrowLeftWingIsBighterThenRightWing(WindDirectionViewModel sut)
    {
        const double WIND_DIRECTION_SOUTH_WEST = 1.25 * Math.PI;
        const double WIND_SPEED_MODERATE = 5.0;

        // --- Act
        sut.HandleAsync(new WeatherUpdated(DummyTemperature, WIND_SPEED_MODERATE, WIND_DIRECTION_SOUTH_WEST), CancellationToken.None);

        Assert.True(sut.ArrowLeftWingColor.Value > sut.ArrowRightWingColor.Value,
            "Expected the left wing to be brighter than the right wing");
    }

    [Theory, AutoFakeData]
    public void Given_WindFromNorthEast__Then_ArrowRightWingIsBighterThenLeftWing(WindDirectionViewModel sut)
    {
        const double WIND_DIRECTION_NORTH_EAST = 0.25 * Math.PI;
        const double WIND_SPEED_MODERATE = 5.0;

        // --- Act
        sut.HandleAsync(new WeatherUpdated(DummyTemperature, WIND_SPEED_MODERATE, WIND_DIRECTION_NORTH_EAST), CancellationToken.None);

        Assert.True(sut.ArrowLeftWingColor.Value < sut.ArrowRightWingColor.Value,
            "Expected the right wing to be brighter than the left wing");
    }

    // Unit test for HandleAsync method of WindDirectionViewModel class for wind speed below gale force, inject SUT into the test method using AutoFakeData attribute
    [Theory, AutoFakeData]
    public void Given_WindSpeedBelowGaleForce__Then_ArrowColorsAreNormal(WindDirectionViewModel sut)
    {
        // --- Act
        sut.HandleAsync(new WeatherUpdated(DummyTemperature, 5.0, 0.0), CancellationToken.None);

        // --- Assert
        Assert.Equal(sut.ArrowLeftWingColor, sut.ArrowRightWingColor);
    }
}