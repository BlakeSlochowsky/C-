public class ClockAngleControllerTests
{ 
    private readonly Moq.Mock<IClockAngleService> _mockService;
    private readonly ClockAngleController _controller;

    public ClockAngleControllerTests()
    {
        _mockService = new Moq.Mock<IClockAngleService>();
        _controller = new ClockAngleController(_mockService.Object);
    }

    [Xunit.Fact]
    public void CalculateTimeAngle_WithTimeString_ReturnsOkWithCorrectResponse()
    {
        var request = new CalculateTimeAngleRequest { Time = "03:00" };
        _mockService.Setup(clockAngleService => clockAngleService.CalculateAngleSum(3, 0)).Returns(90.0);

        var actionResult = _controller.CalculateTimeAngle(request);

        var okActionResult = Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(actionResult);
        var response = Xunit.Assert.IsType<CalculateTimeAngleResponse>(okActionResult.Value);
        Xunit.Assert.Equal(3, response.Hour);
        Xunit.Assert.Equal(0, response.Minute);
        Xunit.Assert.Equal(90.0, response.AngleSum);
        Xunit.Assert.Equal(90.0, response.HourHandAngle);
        Xunit.Assert.Equal(0.0, response.MinuteHandAngle);
    }

    [Xunit.Fact]
    public void CalculateTimeAngle_WithHourAndMinute_ReturnsOkWithCorrectResponse()
    {
        var request = new CalculateTimeAngleRequest { Hour = 3, Minute = 15 };
        _mockService.Setup(clockAngleService => clockAngleService.CalculateAngleSum(3, 15)).Returns(187.5);

        var actionResult = _controller.CalculateTimeAngle(request);

        var okActionResult = Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.OkObjectResult>(actionResult);
        var response = Xunit.Assert.IsType<CalculateTimeAngleResponse>(okActionResult.Value);
        Xunit.Assert.Equal(3, response.Hour);
        Xunit.Assert.Equal(15, response.Minute);
        Xunit.Assert.Equal(187.5, response.AngleSum);
    }

    [Xunit.Fact]
    public void CalculateTimeAngle_WithNullRequest_ReturnsBadRequest()
    {
        var actionResult = _controller.CalculateTimeAngle(null);
        Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actionResult);
    }

    [Xunit.Fact]
    public void CalculateTimeAngle_WithInvalidRequest_ReturnsBadRequest()
    {
        var request = new CalculateTimeAngleRequest();
        var actionResult = _controller.CalculateTimeAngle(request);
        Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actionResult);
    }

    [Xunit.Fact]
    public void CalculateTimeAngle_WithInvalidTimeFormat_ReturnsBadRequest()
    {
        var request = new CalculateTimeAngleRequest { Time = "invalid" };
        _controller.ModelState.AddModelError("Time", "Invalid format");

        var actionResult = _controller.CalculateTimeAngle(request);

        Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actionResult);
    }

    [Xunit.Fact]
    public void CalculateTimeAngle_CallsServiceWithCorrectParameters()
    {
        var request = new CalculateTimeAngleRequest { Time = "15:30" };
        _mockService.Setup(clockAngleService => clockAngleService.CalculateAngleSum(15, 30)).Returns(255.0);

        _controller.CalculateTimeAngle(request);

        _mockService.Verify(clockAngleService => clockAngleService.CalculateAngleSum(15, 30), Moq.Times.Once);
    }

    [Xunit.Fact]
    public void CalculateTimeAngle_WithServiceException_ReturnsBadRequest()
    {
        var request = new CalculateTimeAngleRequest { Time = "invalid" };
        var actionResult = _controller.CalculateTimeAngle(request);
        Xunit.Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestObjectResult>(actionResult);
    }
}
