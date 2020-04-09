using Kurkku.Game;
using Kurkku.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming
{
    class BlankMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {

        }
    }
}
