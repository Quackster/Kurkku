using System;

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
    }
}
