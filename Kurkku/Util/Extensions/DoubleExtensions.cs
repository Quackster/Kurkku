﻿using System;

namespace Kurkku.Util.Extensions
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// Convert double for Habbo client
        /// </summary>
        public static string ToClientValue(this double value)
        {
            return String.Format("{0:0.0}", value);
        }

        /// <summary>
        /// Return an integer - as seconds, to an array of days, hours, minutes and seconds
        /// </summary>
        public static int[] SecondsToTimeUnits(this double num)
        {
            int n = (int)num;
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
