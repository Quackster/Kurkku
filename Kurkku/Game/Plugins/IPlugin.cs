using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public interface IPlugin
    {
        void onEnable();
        void onDisable();
    }
}
