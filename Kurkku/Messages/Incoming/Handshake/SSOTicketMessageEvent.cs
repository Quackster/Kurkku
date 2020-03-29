using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game.Player;
using Kurkku.Messages.Outgoing.Handshake;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming.Handshake
{
    class SSOTicketMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var ssoTicket = request.ReadString();

            if (!player.TryLogin(ssoTicket))
                player.Connection.Disconnect();
        }
    }
}
