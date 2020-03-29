using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game.Player;
using Kurkku.Messages.Outgoing.Handshake;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming.Handshake
{
    class GenerateSecretKeyMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Connection.Send(new SecretKeyComposer("12844543231839046982589043811871065476910599239608903221675649771360705087933"));
        }
    }
}
