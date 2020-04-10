using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class InitMessengerMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new InitMessengerComposer(
                ValueManager.Instance.GetInt("max.friends.normal"),
                ValueManager.Instance.GetInt("max.friends.hc"),
                ValueManager.Instance.GetInt("max.friends.vip"),
                player.Messenger.Categories, 
                player.Messenger.Friends
            ));
        }
    }
}
