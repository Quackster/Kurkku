using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;

namespace Kurkku.Messages.Incoming
{
    class BuddyRequestMessengerEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int userId = UserDao.GetIdByName(request.ReadString());

            if (userId < 1)
                return;

            var targetMessenger = Messenger.GetMessengerData(userId);

            if (targetMessenger == null)
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


        }
    }
}
