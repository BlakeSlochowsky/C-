using System.ComponentModel.DataAnnotations;

namespace ClockAngleApi.Models;

public class CalculateTimeAngleRequest
{
    [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Time must be in HH:mm format (24-hour)")]
    public string? Time { get; set; }

    [Range(0, 23, ErrorMessage = "Hour must be between 0 and 23")]
    public int? Hour { get; set; }

    [Range(0, 59, ErrorMessage = "Minute must be between 0 and 59")]
    public int? Minute { get; set; }

    public bool IsValid()
    {
        bool hasTimeString = !string.IsNullOrWhiteSpace(Time);
        bool hasHourAndMinute = Hour.HasValue && Minute.HasValue;
        return hasTimeString || hasHourAndMinute;
    }

    public (int hour, int minute) Parse()
    {
        if (!string.IsNullOrWhiteSpace(Time))
        {
            return ParseTimeString(Time);
        }

        if (Hour.HasValue && Minute.HasValue)
        {
            return (Hour.Value, Minute.Value);
        }

        throw new ArgumentException("Either Time string or both Hour and Minute must be provided.");
    }

    private static (int hour, int minute) ParseTimeString(string timeStringFromRequest)
    {
        string[] hourAndMinuteParts = timeStringFromRequest.Split(':');
        if (hourAndMinuteParts.Length != 2)
        {
            throw new ArgumentException($"Invalid time format: {timeStringFromRequest}. Expected HH:mm format.");
        }

        bool hourParsed = int.TryParse(hourAndMinuteParts[0], out int parsedHourValue);
        bool minuteParsed = int.TryParse(hourAndMinuteParts[1], out int parsedMinuteValue);
        if (!hourParsed || !minuteParsed)
        {
            throw new ArgumentException($"Invalid time format: {timeStringFromRequest}. Hour and minute must be numeric.");
        }

        if (parsedHourValue < 0 || parsedHourValue > 23)
        {
            throw new ArgumentException($"Hour must be between 0 and 23. Got: {parsedHourValue}");
        }

        if (parsedMinuteValue < 0 || parsedMinuteValue > 59)
        {
            throw new ArgumentException($"Minute must be between 0 and 59. Got: {parsedMinuteValue}");
        }

        return (parsedHourValue, parsedMinuteValue);
    }
}
