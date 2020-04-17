using System.Text;

namespace Kurkku.Util
{
    public class StringUtil
    {
        #region Public methods

        public static Encoding GetEncoding()
        {
            return Encoding.GetEncoding("ISO-8859-1");
        }

        #endregion
    }
}
