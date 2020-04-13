using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class RoomReadyComposer : MessageComposer
    {
        public override short Header => 
            OutgoingEvents.RoomReadyComposer;

        private string modelName;
        private int roomId;

        public RoomReadyComposer(string modelName, int roomId)
        {
            this.modelName = modelName;
            this.roomId = roomId;
        }

        public override void Write()
        {
            m_Data.Add(modelName);
            m_Data.Add(roomId);
        }
    }
}
