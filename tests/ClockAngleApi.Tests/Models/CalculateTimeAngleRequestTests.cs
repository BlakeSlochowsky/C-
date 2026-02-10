using ClockAngleApi.Models;
using FluentAssertions;
using Xunit;

namespace ClockAngleApi.Tests.Models;

public class CalculateTimeAngleRequestTests
{
    [Fact]
    public void Parse_WithTimeString_ReturnsCorrectHourAndMinute()
    {
        var requestWithTimeString = new CalculateTimeAngleRequest { Time = "03:00" };
        var (parsedHourValue, parsedMinuteValue) = requestWithTimeString.Parse();
        parsedHourValue.Should().Be(3);
        parsedMinuteValue.Should().Be(0);
    }

    [Fact]
    public void Parse_WithTimeStringTwentyFourHour_ReturnsCorrectHourAndMinute()
    {
        var requestWithTimeString = new CalculateTimeAngleRequest { Time = "15:30" };
        var (parsedHourValue, parsedMinuteValue) = requestWithTimeString.Parse();
        parsedHourValue.Should().Be(15);
        parsedMinuteValue.Should().Be(30);
    }

    [Fact]
    public void Parse_WithHourAndMinute_ReturnsCorrectValues()
    {
        var requestWithHourAndMinute = new CalculateTimeAngleRequest { Hour = 3, Minute = 15 };
        var (parsedHourValue, parsedMinuteValue) = requestWithHourAndMinute.Parse();
        parsedHourValue.Should().Be(3);
        parsedMinuteValue.Should().Be(15);
    }

    [Fact]
    public void Parse_WithTimeString_ThrowsWhenInvalidFormat()
    {
        var requestWithInvalidTime = new CalculateTimeAngleRequest { Time = "invalid" };
        Assert.Throws<ArgumentException>(() => requestWithInvalidTime.Parse());
    }

    [Fact]
    public void Parse_WithTimeString_ThrowsWhenHourOutOfRange()
    {
        var requestWithHourOutOfRange = new CalculateTimeAngleRequest { Time = "24:00" };
        Assert.Throws<ArgumentException>(() => requestWithHourOutOfRange.Parse());
    }

    [Fact]
    public void Parse_WithTimeString_ThrowsWhenMinuteOutOfRange()
    {
        var requestWithMinuteOutOfRange = new CalculateTimeAngleRequest { Time = "12:60" };
        Assert.Throws<ArgumentException>(() => requestWithMinuteOutOfRange.Parse());
    }

    [Fact]
    public void Parse_WithNeitherTimeNorHourMinute_ThrowsArgumentException()
    {
        var requestWithNoInput = new CalculateTimeAngleRequest();
        Assert.Throws<ArgumentException>(() => requestWithNoInput.Parse());
    }

    [Fact]
    public void IsValid_WithTimeString_ReturnsTrue()
    {
        var requestWithTimeString = new CalculateTimeAngleRequest { Time = "03:00" };
        bool isRequestValid = requestWithTimeString.IsValid();
        isRequestValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_WithHourAndMinute_ReturnsTrue()
    {
        var requestWithHourAndMinute = new CalculateTimeAngleRequest { Hour = 3, Minute = 0 };
        bool isRequestValid = requestWithHourAndMinute.IsValid();
        isRequestValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_WithOnlyHour_ReturnsFalse()
    {
        var requestWithOnlyHour = new CalculateTimeAngleRequest { Hour = 3 };
        bool isRequestValid = requestWithOnlyHour.IsValid();
        isRequestValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_WithOnlyMinute_ReturnsFalse()
    {
        var requestWithOnlyMinute = new CalculateTimeAngleRequest { Minute = 30 };
        bool isRequestValid = requestWithOnlyMinute.IsValid();
        isRequestValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_WithEmptyRequest_ReturnsFalse()
    {
        var requestWithNoInput = new CalculateTimeAngleRequest();
        bool isRequestValid = requestWithNoInput.IsValid();
        isRequestValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("00:00", 0, 0)]
    [InlineData("12:00", 12, 0)]
    [InlineData("23:59", 23, 59)]
    [InlineData("09:15", 9, 15)]
    [InlineData("15:45", 15, 45)]
    public void Parse_WithVariousTimeStrings_ReturnsCorrectValues(string inputTimeString, int expectedHourValue, int expectedMinuteValue)
    {
        var requestWithTimeString = new CalculateTimeAngleRequest { Time = inputTimeString };
        var (parsedHourValue, parsedMinuteValue) = requestWithTimeString.Parse();
        parsedHourValue.Should().Be(expectedHourValue);
        parsedMinuteValue.Should().Be(expectedMinuteValue);
    }
}
