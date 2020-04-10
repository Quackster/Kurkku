using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Util.Extensions;

namespace Kurkku.Messages.Incoming
{
    class InstantChatMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            try
            {
                int userId = request.ReadInt();

                if (!player.Messenger.HasFriend(userId))
                {
                    player.Send(new InstantChatErrorComposer(InstantChatError.NotFriend, player.Details.Id));
                    return;
                }

                var friend = player.Messenger.GetFriend(userId);

                if (!friend.IsOnline)
                {
                    player.Send(new InstantChatErrorComposer(InstantChatError.FriendOffline, player.Details.Id));
                    return;
                }

                friend.Player.Send(new InstantChatComposer(player.Details.Id, request.ReadString().FilterInput(false)));
            }
            catch
            {
                player.Send(new InstantChatErrorComposer(InstantChatError.SendingFailed, player.Details.Id));
            }
        }
    }
}
