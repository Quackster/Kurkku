using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming
{
    class GetFurnitureAliasesMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new FurnitureAliasesComposer());
        }
    }
}
