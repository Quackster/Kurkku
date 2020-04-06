using Kurkku.Game;
using Kurkku.Messages.Outgoing.User;
using Kurkku.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming.User
{
    class UserInfoMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (!player.Authenticated)
                return;

            player.Connection.Send(new UserInfoComposer(player));
            player.Connection.Send(new WelcomeUserComposer());
        }
    }
}
