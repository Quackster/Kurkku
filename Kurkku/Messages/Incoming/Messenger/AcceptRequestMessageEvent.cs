using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Messages.Outgoing.Messenger;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Incoming
{
    public class AcceptRequestsMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int friendsAccepted = request.ReadInt();
            var messenger = player.Messenger;

            for (int i = 0; i < friendsAccepted; i++) 
            {
                int userId = request.ReadInt();

                if (!messenger.HasRequest(userId))
                    continue;

                if (messenger.Friends.Count >= messenger.MaxFriendsAllowed)
                    continue;

                var playerData = PlayerManager.Instance.GetDataById(userId);

                if (playerData == null)
                    continue;

                var targetMessenger = Messenger.GetMessengerData(userId);
                var targetFriend = Messenger.Wrap(playerData);

                targetMessenger.Friends.Add(messenger.MessengerUser);
                messenger.Friends.Add(targetFriend);

                var targetPlayer = PlayerManager.Instance.GetPlayerById(userId);

                if (targetPlayer != null) 
                {
                    targetPlayer.Messenger.QueueUpdate(MessengerUpdateType.AddFriend, messenger.MessengerUser);
                    targetPlayer.Messenger.ForceUpdate();
                }

                MessengerDao.DeleteRequests(player.Details.Id, userId);
                MessengerDao.SaveFriend(new MessengerFriendData
                {
                    FriendId = userId,
                    UserId = player.Details.Id
                });
                MessengerDao.SaveFriend(new MessengerFriendData
                {
                    UserId = userId,
                    FriendId = player.Details.Id
                });

                messenger.QueueUpdate(MessengerUpdateType.AddFriend, targetFriend);
            }

            messenger.ForceUpdate();
        }
    }
}
