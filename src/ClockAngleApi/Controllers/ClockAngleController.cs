using ClockAngleApi.Models;
using ClockAngleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClockAngleApi.Controllers;

[ApiController]
[Route("")]
public class ClockAngleController : ControllerBase
{
    private readonly IClockAngleService _clockAngleService;
    private readonly ILogger<ClockAngleController> _logger;

    public ClockAngleController(IClockAngleService clockAngleService, ILogger<ClockAngleController> logger)
    {
        _clockAngleService = clockAngleService;
        _logger = logger;
    }

    [HttpPost("CalculateTimeAngle")]
    [ProducesResponseType(typeof(CalculateTimeAngleResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult CalculateTimeAngle([FromBody] CalculateTimeAngleRequest request)
    {
        if (request == null)
        {
            return BadRequest(new { error = "Request body is required." });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!request.IsValid())
        {
            return BadRequest(new { error = "Either Time string or both Hour and Minute must be provided." });
        }

        try
        {
            var (parsedHourFromRequest, parsedMinuteFromRequest) = request.Parse();
            double totalAngleSumFromBothHands = _clockAngleService.CalculateAngleSum(parsedHourFromRequest, parsedMinuteFromRequest);

            double hourHandAngleInDegrees = GetHourHandAngle(parsedHourFromRequest, parsedMinuteFromRequest);
            double minuteHandAngleInDegrees = parsedMinuteFromRequest * 6.0;

            var angleCalculationResponse = new CalculateTimeAngleResponse
            {
                Hour = parsedHourFromRequest,
                Minute = parsedMinuteFromRequest,
                AngleSum = totalAngleSumFromBothHands,
                HourHandAngle = hourHandAngleInDegrees,
                MinuteHandAngle = minuteHandAngleInDegrees
            };

            _logger.LogInformation("Calculated angle sum for {Hour}:{Minute:00} = {AngleSum}°", parsedHourFromRequest, parsedMinuteFromRequest, totalAngleSumFromBothHands);

            return Ok(angleCalculationResponse);
        }
        catch (ArgumentException validationException)
        {
            _logger.LogWarning(validationException, "Invalid request received");
            return BadRequest(new { error = validationException.Message });
        }
        catch (Exception unexpectedException)
        {
            _logger.LogError(unexpectedException, "Error calculating time angle");
            return StatusCode(500, new { error = "An error occurred while calculating the time angle." });
        }
    }

    private static double GetHourHandAngle(int hourValue, int minuteValue)
    {
        int hourOnTwelveHourClock = hourValue % 12;
        double degreesPerHourOnClock = 30.0;
        double degreesHourHandMovesPerMinute = 0.5;
        return (hourOnTwelveHourClock * degreesPerHourOnClock) + (minuteValue * degreesHourHandMovesPerMinute);
    }
}
