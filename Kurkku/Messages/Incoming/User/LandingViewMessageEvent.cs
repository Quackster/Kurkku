using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game.Player;
using Kurkku.Messages.Outgoing.User;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming.Handshake
{
    class LandingViewMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            string firstView = request.ReadString();

            if (string.IsNullOrEmpty(firstView))
                return;

            string header = firstView.Split(',')[1];
            string teaser = header.Split(';')[0];

            player.Connection.Send(new LandingViewComposer(header, teaser));
            //player.Connection.Send(new LandingViewComposer("2012-11-09 19:00,hstarsa;2012-11-30 12:00,", "hstarsa"));
        }
    }
}
