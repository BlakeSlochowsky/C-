public class CalculateTimeAngleRequestTests
{
    [Xunit.Fact]
    public void Parse_WithTimeString_ReturnsCorrectHourAndMinute()
    {
        var request = new CalculateTimeAngleRequest { Time = "03:00" };
        var (parsedHour, parsedMinute) = request.Parse();
        Xunit.Assert.Equal(3, parsedHour);
        Xunit.Assert.Equal(0, parsedMinute);
    }

    [Xunit.Fact]
    public void Parse_WithTimeStringTwentyFourHour_ReturnsCorrectHourAndMinute()
    {
        var request = new CalculateTimeAngleRequest { Time = "15:30" };
        var (parsedHour, parsedMinute) = request.Parse();
        Xunit.Assert.Equal(15, parsedHour);
        Xunit.Assert.Equal(30, parsedMinute);
    }

    [Xunit.Fact]
    public void Parse_WithHourAndMinute_ReturnsCorrectValues()
    {
        var request = new CalculateTimeAngleRequest { Hour = 3, Minute = 15 };
        var (parsedHour, parsedMinute) = request.Parse();
        Xunit.Assert.Equal(3, parsedHour);
        Xunit.Assert.Equal(15, parsedMinute);
    }

    [Xunit.Fact]
    public void Parse_WithTimeString_ThrowsWhenInvalidFormat()
    {
        var request = new CalculateTimeAngleRequest { Time = "invalid" };
        Xunit.Assert.Throws<System.ArgumentException>(() => request.Parse());
    }

    [Xunit.Fact]
    public void Parse_WithTimeString_ThrowsWhenHourOutOfRange()
    {
        var request = new CalculateTimeAngleRequest { Time = "24:00" };
        Xunit.Assert.Throws<System.ArgumentException>(() => request.Parse());
    }

    [Xunit.Fact]
    public void Parse_WithTimeString_ThrowsWhenMinuteOutOfRange()
    {
        var request = new CalculateTimeAngleRequest { Time = "12:60" };
        Xunit.Assert.Throws<System.ArgumentException>(() => request.Parse());
    }

    [Xunit.Fact]
    public void Parse_WithNeitherTimeNorHourMinute_ThrowsArgumentException()
    {
        var request = new CalculateTimeAngleRequest();
        Xunit.Assert.Throws<System.ArgumentException>(() => request.Parse());
    }

    [Xunit.Fact]
    public void IsValid_WithTimeString_ReturnsTrue()
    {
        var request = new CalculateTimeAngleRequest { Time = "03:00" };
        bool isRequestValid = request.IsValid();
        Xunit.Assert.True(isRequestValid);
    }

    [Xunit.Fact]
    public void IsValid_WithHourAndMinute_ReturnsTrue()
    {
        var request = new CalculateTimeAngleRequest { Hour = 3, Minute = 0 };
        bool isRequestValid = request.IsValid();
        Xunit.Assert.True(isRequestValid);
    }

    [Xunit.Fact]
    public void IsValid_WithOnlyHour_ReturnsFalse()
    {
        var request = new CalculateTimeAngleRequest { Hour = 3 };
        bool isRequestValid = request.IsValid();
        Xunit.Assert.False(isRequestValid);
    }

    [Xunit.Fact]
    public void IsValid_WithOnlyMinute_ReturnsFalse()
    {
        var request = new CalculateTimeAngleRequest { Minute = 30 };
        bool isRequestValid = request.IsValid();
        Xunit.Assert.False(isRequestValid);
    }

    [Xunit.Fact]
    public void IsValid_WithEmptyRequest_ReturnsFalse()
    {
        var request = new CalculateTimeAngleRequest();
        bool isRequestValid = request.IsValid();
        Xunit.Assert.False(isRequestValid);
    }

    [Xunit.Theory]
    [Xunit.InlineData("00:00", 0, 0)]
    [Xunit.InlineData("12:00", 12, 0)]
    [Xunit.InlineData("23:59", 23, 59)]
    [Xunit.InlineData("09:15", 9, 15)]
    [Xunit.InlineData("15:45", 15, 45)]
    public void Parse_WithVariousTimeStrings_ReturnsCorrectValues(string inputTimeString, int expectedHour, int expectedMinute)
    {
        var request = new CalculateTimeAngleRequest { Time = inputTimeString };
        var (parsedHour, parsedMinute) = request.Parse();
        Xunit.Assert.Equal(expectedHour, parsedHour);
        Xunit.Assert.Equal(expectedMinute, parsedMinute);
    }
}
