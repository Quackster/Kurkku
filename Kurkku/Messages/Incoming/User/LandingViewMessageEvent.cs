using Kurkku.Game;
using Kurkku.Network.Streams;
using Kurkku.Messages.Outgoing;

namespace Kurkku.Messages.Incoming
{
    class LandingViewMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            string first = request.ReadString();

            if (string.IsNullOrEmpty(first))
            {
                player.Send(new LandingViewComposer("", ""));
                return;
            }

            string value = first.Split(',')[1];

            //player.Connection.Send(new LandingViewComposer(value, value.Split(';')[0]));
            //player.Connection.Send(new LandingViewComposer("2012-11-09 19:00,hstarsa;2012-11-30 12:00,", "hstarsa"));
        }
    }
}
