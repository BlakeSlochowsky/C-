public class CalculateTimeAngleRequest
{ 
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Time must be in HH:mm format (24-hour)")]
    public string? Time { get; set; }

    [System.ComponentModel.DataAnnotations.Range(0, 23, ErrorMessage = "Hour must be between 0 and 23")]
    public int? Hour { get; set; }

    [System.ComponentModel.DataAnnotations.Range(0, 59, ErrorMessage = "Minute must be between 0 and 59")]
    public int? Minute { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Time) || (Hour.HasValue && Minute.HasValue);
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

        throw new System.ArgumentException("Either Time string or both Hour and Minute must be provided.");
    }

    private static (int hour, int minute) ParseTimeString(string timeString)
    {
        var timeStringComponents = timeString.Split(':');
        if (timeStringComponents.Length != 2)
        {
            throw new System.ArgumentException($"Invalid time format: {timeString}. Expected HH:mm format.");
        }

        if (!int.TryParse(timeStringComponents[0], out int parsedHour) || !int.TryParse(timeStringComponents[1], out int parsedMinute))
        {
            throw new System.ArgumentException($"Invalid time format: {timeString}. Hour and minute must be numeric.");
        }

        if (parsedHour < 0 || parsedHour > 23)
        {
            throw new System.ArgumentException($"Hour must be between 0 and 23. Got: {parsedHour}");
        }

        if (parsedMinute < 0 || parsedMinute > 59)
        {
            throw new System.ArgumentException($"Minute must be between 0 and 59. Got: {parsedMinute}");
        }

        return (parsedHour, parsedMinute);
    }
}
