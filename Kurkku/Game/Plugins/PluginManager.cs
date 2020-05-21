using Kurkku.Storage.Database.Access;
using Kurkku.Util.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using log4net;
using System.Reflection;

namespace Kurkku.Game
{
    public class PluginManager : ILoadable
    {
        #region Fields

        public static readonly PluginManager Instance = new PluginManager();
        private List<PluginProxy> plugins;
        private string pluginDictionary = "plugins";

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Properties

        public IPlugin[] Plugins => plugins.Select(x => x.Plugin).ToArray();

        #endregion

        #region Constructors

        public void Load()
        {
            ReloadPlugins();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Reload plugin handler
        /// </summary>
        public void ReloadPlugins()
        {
            if (!Directory.Exists(pluginDictionary))
                Directory.CreateDirectory(pluginDictionary);

            bool isReload = plugins != null;

            if (plugins != null)
            {
                plugins.ForEach(x => x.Plugin.onDisable());
                plugins = null;
            }

            if (plugins == null)
            {
                plugins = new List<PluginProxy>();

                foreach (string file in Directory.GetFiles(pluginDictionary))
                {
                    if (Path.GetExtension(file) != ".dll")
                        continue;

                    log.Info($"Loading {Path.GetFileName(file)} as a plugin");
                    plugins.Add(new PluginProxy(file));
                }
            }

            plugins.ForEach(x => x.Plugin.onEnable());
        }

        #endregion
    }
}
