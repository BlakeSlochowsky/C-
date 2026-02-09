public class ClockAngleController : Microsoft.AspNetCore.Mvc.ControllerBase
{ 
    private readonly IClockAngleService _clockAngleService;

    public ClockAngleController(IClockAngleService clockAngleService)
    {
        _clockAngleService = clockAngleService;
    }

    [Microsoft.AspNetCore.Mvc.HttpPost("CalculateTimeAngle")]
    public Microsoft.AspNetCore.Mvc.IActionResult CalculateTimeAngle([Microsoft.AspNetCore.Mvc.FromBody] CalculateTimeAngleRequest request)
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
            var (parsedHour, parsedMinute) = request.Parse();
            double calculatedAngleSum = _clockAngleService.CalculateAngleSum(parsedHour, parsedMinute);

            int normalizedTwelveHourFormat = parsedHour % 12;
            double calculatedHourHandAngle = (normalizedTwelveHourFormat * 30.0) + (parsedMinute * 0.5);
            double calculatedMinuteHandAngle = parsedMinute * 6.0;

            var response = new CalculateTimeAngleResponse
            {
                Hour = parsedHour,
                Minute = parsedMinute,
                AngleSum = calculatedAngleSum,
                HourHandAngle = calculatedHourHandAngle,
                MinuteHandAngle = calculatedMinuteHandAngle
            };

            return Ok(response);
        }
        catch (System.ArgumentException argumentException)
        {
            return BadRequest(new { error = argumentException.Message });
        }
        catch (System.Exception)
        {
            return StatusCode(500, new { error = "An error occurred while calculating the time angle." });
        }
    }
}
