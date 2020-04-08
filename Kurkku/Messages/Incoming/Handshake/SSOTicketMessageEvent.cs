using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
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
