using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Messages.Outgoing.Messenger;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Incoming
{
    class BuddyRequestMessageEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int userId = UserDao.GetIdByName(request.ReadString());

            if (userId < 1)
                return;

            var targetMessenger = Messenger.GetMessengerData(userId);
            var targetPlayer = PlayerManager.Instance.GetPlayerById(userId);

            if (targetMessenger == null || 
                targetMessenger.HasFriend(player.Details.Id) || 
                targetMessenger.HasRequest(player.Details.Id))
                return;

            if (!targetMessenger.FriendRequestsEnabled)
            {
                player.Send(new MessengerRequestErrorComposer(MessengerRequestError.FriendRequestsDisabled));
                return;
            }

            if (player.Messenger.Friends.Count >= player.Messenger.MaxFriendsAllowed)
            {
                player.Send(new MessengerRequestErrorComposer(MessengerRequestError.FriendListFull));
                return;
            }

            var messengerRequest = new MessengerRequestData
            {
                FriendId = player.Details.Id,
                UserId = userId
            };

            MessengerDao.SaveRequest(messengerRequest);

            targetMessenger.Requests.Add(player.Messenger.MessengerUser);

            if (targetPlayer != null)
                targetPlayer.Send(new MessengerRequestComposer(player.Details));
        }
    }
}
