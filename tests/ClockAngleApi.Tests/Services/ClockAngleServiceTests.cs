using ClockAngleApi.Services;
using FluentAssertions;
using Xunit;

namespace ClockAngleApi.Tests.Services;

public class ClockAngleServiceTests
{
    private readonly ClockAngleService _clockAngleServiceUnderTest;

    public ClockAngleServiceTests()
    {
        _clockAngleServiceUnderTest = new ClockAngleService();
    }

    [Fact]
    public void CalculateAngleSum_AtMidnight_ReturnsZero()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(0, 0);
        calculatedAngleSum.Should().Be(0.0);
    }

    [Fact]
    public void CalculateAngleSum_AtNoon_ReturnsZero()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(12, 0);
        calculatedAngleSum.Should().Be(0.0);
    }

    [Fact]
    public void CalculateAngleSum_AtThreeOClock_ReturnsNinety()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(3, 0);
        calculatedAngleSum.Should().Be(90.0);
    }

    [Fact]
    public void CalculateAngleSum_AtSixOClock_ReturnsOneEighty()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(6, 0);
        calculatedAngleSum.Should().Be(180.0);
    }

    [Fact]
    public void CalculateAngleSum_AtNineOClock_ReturnsTwoSeventy()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(9, 0);
        calculatedAngleSum.Should().Be(270.0);
    }

    [Fact]
    public void CalculateAngleSum_AtThreeThirty_HourHandMoves()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(3, 30);
        calculatedAngleSum.Should().Be(285.0);
    }

    [Fact]
    public void CalculateAngleSum_AtFifteenMinutes_HourHandMovesSlightly()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(3, 15);
        calculatedAngleSum.Should().Be(187.5);
    }

    [Fact]
    public void CalculateAngleSum_AtFortyFiveMinutes_HourHandMoves()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(3, 45);
        calculatedAngleSum.Should().Be(382.5);
    }

    [Fact]
    public void CalculateAngleSum_WithTwentyFourHourFormat_NormalizesCorrectly()
    {
        double angleSumForFifteenHundred = _clockAngleServiceUnderTest.CalculateAngleSum(15, 0);
        double angleSumForThreeOClock = _clockAngleServiceUnderTest.CalculateAngleSum(3, 0);
        angleSumForFifteenHundred.Should().Be(angleSumForThreeOClock).And.Be(90.0);
    }

    [Fact]
    public void CalculateAngleSum_AtOneOClock_ReturnsThirty()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(1, 0);
        calculatedAngleSum.Should().Be(30.0);
    }

    [Fact]
    public void CalculateAngleSum_AtElevenFiftyNine_ReturnsExpectedSum()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(11, 59);
        calculatedAngleSum.Should().Be(713.5);
    }

    [Fact]
    public void CalculateAngleSum_AtTwentyThreeHours_NormalizesToEleven()
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(23, 0);
        calculatedAngleSum.Should().Be(330.0);
    }

    [Theory]
    [InlineData(0, 0, 0.0)]
    [InlineData(3, 0, 90.0)]
    [InlineData(6, 0, 180.0)]
    [InlineData(9, 0, 270.0)]
    [InlineData(12, 0, 0.0)]
    [InlineData(1, 0, 30.0)]
    [InlineData(2, 0, 60.0)]
    [InlineData(4, 0, 120.0)]
    [InlineData(5, 0, 150.0)]
    public void CalculateAngleSum_AtVariousHours_ReturnsExpectedValues(int inputHourValue, int inputMinuteValue, double expectedTotalAngleInDegrees)
    {
        double calculatedAngleSum = _clockAngleServiceUnderTest.CalculateAngleSum(inputHourValue, inputMinuteValue);
        calculatedAngleSum.Should().Be(expectedTotalAngleInDegrees);
    }
}
