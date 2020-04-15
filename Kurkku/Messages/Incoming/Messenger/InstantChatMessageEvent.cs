using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;

namespace Kurkku.Messages.Incoming
{
    class InstantChatMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int userId = request.ReadInt();

            try
            {
                if (!player.Messenger.HasFriend(userId))
                {
                    player.Send(new InstantChatErrorComposer(InstantChatError.NotFriend, userId));
                    return;
                }

                var friend = player.Messenger.GetFriend(userId);
                var chatMessage = request.ReadString().FilterInput(false);

                var chatMessageData = new MessengerChatData
                {
                    UserId = player.Details.Id,
                    FriendId = userId,
                    Message = chatMessage,
                    IsRead = friend.IsOnline
                };

                MessengerDao.SaveMessage(chatMessageData);

                if (!friend.IsOnline)
                {
                    player.Send(new InstantChatErrorComposer(InstantChatError.FriendOffline, userId));
                    return;
                }

                friend.Player.Send(new InstantChatComposer(player.Details.Id, chatMessage));
            }
            catch
            {
                player.Send(new InstantChatErrorComposer(InstantChatError.SendingFailed, userId));
            }
        }
    }
}
