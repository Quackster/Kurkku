using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class ScrGetUserInfoMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new ScrSendUserInfoComposer());
        }
    }
}
