using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game.Player;
using Kurkku.Messages.Outgoing.Handshake;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming.Handshake
{
    class InitCryptoMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Connection.Send(new InitCryptoComposer("1e9d1203d2203d3dd9ddcb192ccf0a01"));
        }
    }
}
