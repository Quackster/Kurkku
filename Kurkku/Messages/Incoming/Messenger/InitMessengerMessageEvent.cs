using Kurkku.Game;
using Kurkku.Messages.Outoing.Messenger;
using Kurkku.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming.Messenger
{
    class InitMessengerMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Connection.Send(new InitMessengerComposer(
                player.Messenger.Categories, 
                player.Messenger.Friends
            ));
        }
    }
}
