using Kurkku.Game;
using Kurkku.Messages.Outgoing.User;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming.Handshake
{
    class LandingViewMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            string first = request.ReadString();

            if (string.IsNullOrEmpty(first))
            {
                player.Connection.Send(new BlankComposer("", ""));
                return;
            }

            string value = first.Split(',')[1];

            //player.Connection.Send(new LandingViewComposer(value, value.Split(';')[0]));
            //player.Connection.Send(new LandingViewComposer("2012-11-09 19:00,hstarsa;2012-11-30 12:00,", "hstarsa"));
        }
    }
}
