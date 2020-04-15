using Kurkku.Game;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    public class FlatListComposer : IMessageComposer
    {
        private int signifier;
        private List<Room> roomList;

        public FlatListComposer(int signifier, List<Room> roomList)
        {
            this.signifier = signifier;
            this.roomList = roomList;
        }

        public override void Write()
        {
            m_Data.Add(0);
            m_Data.Add(Convert.ToString(this.signifier));
            m_Data.Add(roomList.Count);

            foreach (Room room in roomList)
            {
                FlatListComposer.Compose(this, room.Data);
            }

            m_Data.Add(0);
            m_Data.Add(0);
            m_Data.Add(false);
        }

        public static void Compose(IMessageComposer messageComposer, RoomData room)
        {
            messageComposer.Data.Add(room.Id);
            messageComposer.Data.Add(room.Name);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(room.OwnerId);
            messageComposer.Data.Add(room.OwnerData == null ? string.Empty : room.OwnerData.Name);
            messageComposer.Data.Add((int)room.Status);
            messageComposer.Data.Add(room.UsersNow);
            messageComposer.Data.Add(room.UsersMax);
            messageComposer.Data.Add(room.Description);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(room.Category.IsTradingAllowed ? 2 : 0); // can category trade?
            messageComposer.Data.Add(room.Rating);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(room.Category.Id);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add("");
            messageComposer.Data.Add(0); // tags
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(true);
            messageComposer.Data.Add(0);
            messageComposer.Data.Add(0);
        }
    }
}
