using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class ScrGetUserInfoMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new ScrSendUserInfoComposer(player.Subscription));
            player.Send(new ActivityPointNotificationComposer());
        }
    }
}
