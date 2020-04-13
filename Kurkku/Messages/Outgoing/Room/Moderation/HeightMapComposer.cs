using Kurkku.Messages.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class YouAreControllerComposer : IMessageComposer
    {
        private int rightsLevel;

        public YouAreControllerComposer(int rightsLevel)
        {
            this.rightsLevel = rightsLevel;
        }

        public override void Write()
        {
            m_Data.Add(rightsLevel);
        }
    }
}
