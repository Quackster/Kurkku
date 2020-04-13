using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class RoomPropertyComposer : IMessageComposer
    {
        private string key;
        private string value;

        public RoomPropertyComposer(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public override void Write()
        {
            m_Data.Add(key);
            m_Data.Add(value);
        }
    }
}
