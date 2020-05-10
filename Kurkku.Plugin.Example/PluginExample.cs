using Kurkku.Game;
using log4net;
using System;
using System.Reflection;

namespace Plugin.Example
{
    public class PluginExample : IPlugin
    {
        #region Fields

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Public methods

        public void onEnable()
        {
            log.Info("Enabling Plugin Example");
        }

        public void onDisable()
        {
            log.Info("Disposing Plugin Example");
        }

        #endregion
    }
}
