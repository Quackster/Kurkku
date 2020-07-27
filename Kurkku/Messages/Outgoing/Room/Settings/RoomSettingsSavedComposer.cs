using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    public class RoomSettingsSavedComposer : IMessageComposer
    {
        private int roomId;

        public RoomSettingsSavedComposer(int roomId)
        {
            this.roomId = roomId;
        }

        public override void Write()
        {
            m_Data.Add(this.roomId);
        }
    }
}
