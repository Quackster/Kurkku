using Kurkku.Game;
using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class FlatListComposer : MessageComposer
    {
        private int signifier;
        private List<Room> roomList;

        public override short Header
        {
            get { return OutgoingEvents.FlatListComposer; }
        }

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
