using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;

namespace Kurkku.Messages.Incoming
{
    class ToggleRoomMuteMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {     
            var room = player.RoomUser.Room;
            
            if (room == null)
                return;


            room.Data.IsMuted = !room.Data.IsMuted;

            player.Send(new RoomMuteSettingsComposer(room.Data.IsMuted));
            RoomDao.SaveRoom(room.Data);
        }
    }
}
