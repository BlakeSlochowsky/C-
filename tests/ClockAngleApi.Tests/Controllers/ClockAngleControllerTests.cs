using ClockAngleApi.Controllers;
using ClockAngleApi.Models;
using ClockAngleApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ClockAngleApi.Tests.Controllers;

public class ClockAngleControllerTests
{
    private readonly Mock<IClockAngleService> _mockService;
    private readonly Mock<ILogger<ClockAngleController>> _mockLogger;
    private readonly ClockAngleController _controller;

    public ClockAngleControllerTests()
    {
        _mockService = new Mock<IClockAngleService>();
        _mockLogger = new Mock<ILogger<ClockAngleController>>();
        _controller = new ClockAngleController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public void CalculateTimeAngle_WithTimeString_ReturnsOkWithCorrectResponse()
    {
        var requestWithTimeString = new CalculateTimeAngleRequest { Time = "03:00" };
        _mockService.Setup(clockAngleService => clockAngleService.CalculateAngleSum(3, 0)).Returns(90.0);

        var actionResultReturned = _controller.CalculateTimeAngle(requestWithTimeString);

        var okActionResultFromController = actionResultReturned.Should().BeOfType<OkObjectResult>().Subject;
        var responseBodyFromEndpoint = okActionResultFromController.Value.Should().BeOfType<CalculateTimeAngleResponse>().Subject;
        responseBodyFromEndpoint.Hour.Should().Be(3);
        responseBodyFromEndpoint.Minute.Should().Be(0);
        responseBodyFromEndpoint.AngleSum.Should().Be(90.0);
        responseBodyFromEndpoint.HourHandAngle.Should().Be(90.0);
        responseBodyFromEndpoint.MinuteHandAngle.Should().Be(0.0);
    }

    [Fact]
    public void CalculateTimeAngle_WithHourAndMinute_ReturnsOkWithCorrectResponse()
    {
        var requestWithHourAndMinute = new CalculateTimeAngleRequest { Hour = 3, Minute = 15 };
        _mockService.Setup(clockAngleService => clockAngleService.CalculateAngleSum(3, 15)).Returns(187.5);

        var actionResultReturned = _controller.CalculateTimeAngle(requestWithHourAndMinute);

        var okActionResultFromController = actionResultReturned.Should().BeOfType<OkObjectResult>().Subject;
        var responseBodyFromEndpoint = okActionResultFromController.Value.Should().BeOfType<CalculateTimeAngleResponse>().Subject;
        responseBodyFromEndpoint.Hour.Should().Be(3);
        responseBodyFromEndpoint.Minute.Should().Be(15);
        responseBodyFromEndpoint.AngleSum.Should().Be(187.5);
    }

    [Fact]
    public void CalculateTimeAngle_WithNullRequest_ReturnsBadRequest()
    {
        var actionResultReturned = _controller.CalculateTimeAngle(null);
        actionResultReturned.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void CalculateTimeAngle_WithInvalidRequest_ReturnsBadRequest()
    {
        var requestWithNoInput = new CalculateTimeAngleRequest();
        var actionResultReturned = _controller.CalculateTimeAngle(requestWithNoInput);
        actionResultReturned.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void CalculateTimeAngle_WithInvalidTimeFormat_ReturnsBadRequest()
    {
        var requestWithInvalidTime = new CalculateTimeAngleRequest { Time = "invalid" };
        _controller.ModelState.AddModelError("Time", "Invalid format");

        var actionResultReturned = _controller.CalculateTimeAngle(requestWithInvalidTime);

        actionResultReturned.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void CalculateTimeAngle_CallsServiceWithCorrectParameters()
    {
        var requestWithTimeString = new CalculateTimeAngleRequest { Time = "15:30" };
        _mockService.Setup(clockAngleService => clockAngleService.CalculateAngleSum(15, 30)).Returns(255.0);

        _controller.CalculateTimeAngle(requestWithTimeString);

        _mockService.Verify(clockAngleService => clockAngleService.CalculateAngleSum(15, 30), Times.Once);
    }

    [Fact]
    public void CalculateTimeAngle_WithInvalidTimeParse_ReturnsBadRequest()
    {
        var requestWithInvalidTime = new CalculateTimeAngleRequest { Time = "invalid" };
        var actionResultReturned = _controller.CalculateTimeAngle(requestWithInvalidTime);
        actionResultReturned.Should().BeOfType<BadRequestObjectResult>();
    }
}
