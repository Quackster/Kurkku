using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class UserInfoMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (!player.Authenticated)
                return;

            player.Send(new UserInfoComposer(player));
            player.Send(new WelcomeUserComposer());
        }
    }
}
