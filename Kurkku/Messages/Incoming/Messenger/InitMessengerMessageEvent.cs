using Kurkku.Game;
using Kurkku.Messages.Outoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class InitMessengerMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new InitMessengerComposer(
                player.Messenger.Categories, 
                player.Messenger.Friends
            ));
        }
    }
}
