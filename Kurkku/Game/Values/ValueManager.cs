using Kurkku.Util.Extensions;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public class ValueManager
    {
        #region Fields

        public static readonly ValueManager Instance = new ValueManager();

        #endregion

        #region Properties

        private Dictionary<string, string> ClientValues
        {
            get; set;
        }

        #endregion

        #region Constructors

        public ValueManager()
        {
            SetDefaultValues();
        }

        #endregion

        #region Private methods

        private void SetDefaultValues()
        {
            ClientValues = new Dictionary<string, string>();
            ClientValues["max.friends.normal"] = "300";
            ClientValues["max.friends.hc"] = "600";
            ClientValues["max.friends.vip"] = "1100";
        }


        #endregion

        #region Public methods

        /// <summary>
        /// Get the integer value by key
        /// </summary>
        public int GetInt(string key)
        {
            if (ClientValues.TryGetValue(key, out string value))
                return value.IsNumeric() ? int.Parse(value) : 0;

            return 0;
        }

        /// <summary>
        /// Get the string value by key
        /// </summary>
        public string GetString(string key)
        {
            return ClientValues.TryGetValue(key, out string value) ? value : null;
        }

        #endregion
    }
}
