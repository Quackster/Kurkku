using Kurkku.Game;
using System;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    class FlatListComposer : IMessageComposer
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
                RoomInfoComposer.Compose(this, room.Data);
            }

            m_Data.Add(0);
            m_Data.Add(0);
            m_Data.Add(false);
        }
    }
}
