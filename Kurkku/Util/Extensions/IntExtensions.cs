namespace Kurkku.Util.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Return an integer - as seconds, to an array of days, hours, minutes and seconds
        /// </summary>
        public static int[] SecondsToTimeUnits(this int num)
        {
            int n = num;
            int day = n / (24 * 3600);

            n = n % (24 * 3600);
            int hour = n / 3600;

            n %= 3600;
            int minutes = n / 60;

            n %= 60;
            int seconds = n;

            return new int[] { day, hour, minutes, seconds };
        }
    }
}
