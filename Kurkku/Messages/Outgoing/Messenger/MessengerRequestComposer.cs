using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing.Messenger
{
    class MessengerRequestComposer : MessageComposer
    {
        private PlayerData m_PlayerData;

        public override short Header
        {
            get { return OutgoingEvents.MessengerRequestComposer; }
        }

        public MessengerRequestComposer(PlayerData playerData)
        {
            m_PlayerData = playerData;
        }

        public override void Write()
        {
            m_Data.Add(m_PlayerData.Id);
            m_Data.Add(m_PlayerData.Name);
            m_Data.Add(m_PlayerData.Figure);
        }
    }
}
