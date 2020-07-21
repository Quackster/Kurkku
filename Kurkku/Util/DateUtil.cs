using System;

namespace Kurkku.Util
{
    public class DateUtil
    {
        /// <summary>
        /// Get the current unix timestamp.
        /// </summary>
        public static long GetUnixTimestamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        /// <summary>
        /// Get the current unix timestamp.
        /// </summary>
        public static long GetUnixTimestampInMillis()
        {
            return DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }
    }
}
