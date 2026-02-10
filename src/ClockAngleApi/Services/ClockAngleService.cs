namespace ClockAngleApi.Services;

public class ClockAngleService : IClockAngleService
{
    public double CalculateAngleSum(int hourValue, int minuteValue)
    {
        int hourOnTwelveHourClock = hourValue % 12;
        double degreesPerHourOnClockFace = 30.0;
        double degreesHourHandMovesPerMinute = 0.5;
        double degreesMinuteHandMovesPerMinute = 6.0;

        double hourHandAngleInDegrees = (hourOnTwelveHourClock * degreesPerHourOnClockFace) + (minuteValue * degreesHourHandMovesPerMinute);
        double minuteHandAngleInDegrees = minuteValue * degreesMinuteHandMovesPerMinute;

        return hourHandAngleInDegrees + minuteHandAngleInDegrees;
    }
}
