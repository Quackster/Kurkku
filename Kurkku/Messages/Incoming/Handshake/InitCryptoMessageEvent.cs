using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class InitCryptoMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.Authenticated)
            {
                player.Connection.Close();
                return;
            }

            player.Send(new InitCryptoComposer("1e9d1203d2203d3dd9ddcb192ccf0a01"));
        }
    }
}
