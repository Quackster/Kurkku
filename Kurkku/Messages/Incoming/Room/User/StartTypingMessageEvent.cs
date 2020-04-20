using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    public class StartTypingMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            if (player.RoomUser.Room == null)
                return;

            player.RoomUser.TimerManager.StartSpeechBubbleTimer();
            player.Send(new TypingStatusComposer(player.RoomUser.InstanceId, true));
        }
    }
}
