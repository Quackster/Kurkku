using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    class FollowFriendMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int friendId = request.ReadInt();

            if (!player.Messenger.HasFriend(friendId))
            {
                player.Send(new FollowBuddyErrorComposer(FollowBuddyError.NotFriend));
                return;
            }

            var friend = player.Messenger.GetFriend(friendId);

            if (!friend.IsOnline)
            {
                player.Send(new FollowBuddyErrorComposer(FollowBuddyError.Offline));
                return;
            }

            if (!friend.InRoom)
            {
                player.Send(new FollowBuddyErrorComposer(FollowBuddyError.HotelView));
                return;
            }

            Room room = friend.Player.RoomUser.Room;
            player.Send(new RoomForwardComposer(room.Data.Id, room.Data.IsPublicRoom));
        }
    }
}
