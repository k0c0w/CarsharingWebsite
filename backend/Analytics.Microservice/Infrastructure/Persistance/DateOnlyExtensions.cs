namespace Persistence;

public static class DateOnlyExtensions
{
    private readonly static int _unixEpochDateDayNumber = DateOnly.FromDateTime(DateTime.UnixEpoch).DayNumber;

    private const long SECONDS_IN_DAY = 86400;

    public static long ToUnixTimestamp(this DateOnly dateOnly)
    {
        var daysSinceUnixEpoch = dateOnly.DayNumber - _unixEpochDateDayNumber;

        return daysSinceUnixEpoch * SECONDS_IN_DAY;
    }
}