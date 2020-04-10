using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Messages.Outgoing.Messenger;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Incoming
{
    class RemoveFriendMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int friendsToDelete = request.ReadInt();
            var messenger = player.Messenger;

            for (int i = 0; i < friendsToDelete; i++)
            {
                int userId = request.ReadInt();

                if (!messenger.HasFriend(userId))
                    continue;

                if (messenger.Friends.Count >= messenger.MaxFriendsAllowed)
                    continue;

                var playerData = PlayerManager.Instance.GetDataById(userId);

                if (playerData == null)
                    continue;

                var targetMessenger = Messenger.GetMessengerData(userId);

                targetMessenger.RemoveFriend(player.Details.Id);
                messenger.RemoveFriend(userId);

                var targetPlayer = PlayerManager.Instance.GetPlayerById(userId);

                if (targetPlayer != null)
                {
                    targetPlayer.Messenger.QueueUpdate(MessengerUpdateType.RemoveFriend, messenger.MessengerUser);
                    targetPlayer.Messenger.ForceUpdate();
                }

                MessengerDao.DeleteRequests(player.Details.Id, userId);
                MessengerDao.DeleteFriends(player.Details.Id, userId);

                messenger.QueueUpdate(MessengerUpdateType.RemoveFriend, Messenger.Wrap(playerData));
            }

            messenger.ForceUpdate();
        }
    }
}
