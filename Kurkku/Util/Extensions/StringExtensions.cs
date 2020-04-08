using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Util.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Filter harmful injectable characters
        /// </summary>
        public static string FilterInput(this string str, bool filterNewline = true)
        {
            str = str.Replace((char)1, ' ');
            str = str.Replace((char)2, ' ');
            str = str.Replace((char)3, ' ');
            str = str.Replace((char)9, ' ');

            if (filterNewline == true)
            {
                str = str.Replace((char)10, ' ');
                str = str.Replace((char)13, ' ');
            }

            return str;
        }
    }
}
