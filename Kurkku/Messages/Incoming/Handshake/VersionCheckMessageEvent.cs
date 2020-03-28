using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game.Player;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming.Handshake
{
    class VersionCheckMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            var swfRelease = request.ReadString();
            player.Log.Debug($"Received request: {swfRelease}");
        }
    }
}
