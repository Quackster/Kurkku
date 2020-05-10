using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Kurkku.Game
{
    public class PluginProxy
    {
        #region Properties

        public IPlugin Plugin { get; }
        public Assembly PluginLibrary { get; }

        #endregion

        #region Constructors

        public PluginProxy(string file)
        {
            PluginLibrary = Assembly.LoadFrom(file);

            foreach (Type pluginType in PluginLibrary.GetTypes())
            {
                var typeInterface = pluginType.GetInterface("Kurkku.Game.IPlugin", true);

                if (typeInterface == null)
                    continue;

                Plugin = (IPlugin)Activator.CreateInstance(pluginType);
            }
        }

        #endregion
    }
}
