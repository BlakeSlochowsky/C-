public class ClockAngleService : IClockAngleService
{
    public double CalculateAngleSum(int hour, int minute)
    {
        int normalizedHour = hour % 12;
        double hourAngle = (normalizedHour * 30.0) + (minute * 0.5);
        double minuteAngle = minute * 6.0;
        return hourAngle + minuteAngle;
    }
}
