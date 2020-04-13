using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class FurnitureAliasesComposer : MessageComposer
    {
        public override short Header => 
            OutgoingEvents.FurnitureAliasesComposer;

        public override void Write()
        {
            m_Data.Add(0);
        }
    }
}
