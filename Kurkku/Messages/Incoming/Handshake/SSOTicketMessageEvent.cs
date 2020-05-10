using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class SSOTicketMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.Authenticated)
            {
                player.Connection.Close();
                return;
            }

            var ssoTicket = request.ReadString();

            if (!player.TryLogin(ssoTicket))
                player.Connection.Disconnect();
        }
    }
}
