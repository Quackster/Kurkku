using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class FurnitureAliasesComposer : IMessageComposer
    {
        public override void Write()
        {
            m_Data.Add(0);
        }
    }
}
