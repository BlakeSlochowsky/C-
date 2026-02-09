public class ClockAngleServiceTests
{ 
    private readonly ClockAngleService _service;

    public ClockAngleServiceTests()
    {
        _service = new ClockAngleService();
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtMidnight_ReturnsZero()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(0, 0);
        Xunit.Assert.Equal(0.0, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtNoon_ReturnsZero()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(12, 0);
        Xunit.Assert.Equal(0.0, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtThreeOClock_ReturnsNinety()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(3, 0);
        Xunit.Assert.Equal(90.0, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtSixOClock_ReturnsOneEighty()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(6, 0);
        Xunit.Assert.Equal(180.0, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtNineOClock_ReturnsTwoSeventy()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(9, 0);
        Xunit.Assert.Equal(270.0, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtThreeThirty_HourHandMoves()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(3, 30);
        Xunit.Assert.Equal(285.0, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtFifteenMinutes_HourHandMovesSlightly()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(3, 15);
        Xunit.Assert.Equal(187.5, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtFortyFiveMinutes_HourHandMoves()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(3, 45);
        Xunit.Assert.Equal(382.5, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_WithTwentyFourHourFormat_NormalizesCorrectly()
    {
        double twentyFourHourFormatResult = _service.CalculateAngleSum(15, 0);
        double twelveHourFormatResult = _service.CalculateAngleSum(3, 0);
        Xunit.Assert.Equal(twelveHourFormatResult, twentyFourHourFormatResult);
        Xunit.Assert.Equal(90.0, twentyFourHourFormatResult);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtOneOClock_ReturnsThirty()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(1, 0);
        Xunit.Assert.Equal(30.0, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtElevenFiftyNine_HourHandAlmostAtTwelve()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(11, 59);
        Xunit.Assert.Equal(713.5, calculatedAngleSum);
    }

    [Xunit.Fact]
    public void CalculateAngleSum_AtTwentyThreeHours_NormalizesToEleven()
    {
        double calculatedAngleSum = _service.CalculateAngleSum(23, 0);
        Xunit.Assert.Equal(330.0, calculatedAngleSum);
    }

    [Xunit.Theory]
    [Xunit.InlineData(0, 0, 0.0)]
    [Xunit.InlineData(3, 0, 90.0)]
    [Xunit.InlineData(6, 0, 180.0)]
    [Xunit.InlineData(9, 0, 270.0)]
    [Xunit.InlineData(12, 0, 0.0)]
    [Xunit.InlineData(1, 0, 30.0)]
    [Xunit.InlineData(2, 0, 60.0)]
    [Xunit.InlineData(4, 0, 120.0)]
    [Xunit.InlineData(5, 0, 150.0)]
    public void CalculateAngleSum_AtVariousHours_ReturnsExpectedValues(int inputHour, int inputMinute, double expectedAngleSum)
    {
        double calculatedAngleSum = _service.CalculateAngleSum(inputHour, inputMinute);
        Xunit.Assert.Equal(expectedAngleSum, calculatedAngleSum);
    }
}
