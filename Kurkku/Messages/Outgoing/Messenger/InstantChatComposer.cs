using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    public class InstantChatComposer : MessageComposer
    {
        private int fromId;
        private string message;

        public InstantChatComposer(int fromId, string message)
        {
            this.fromId = fromId;
            this.message = message;
        }

        public override short Header
        {
            get { return OutgoingEvents.InstantChatComposer; }
        }

        public override void Write()
        {
            m_Data.Add(fromId);
            m_Data.Add(message);
            m_Data.Add(0);
        }
    }
}