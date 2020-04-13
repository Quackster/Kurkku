using Kurkku.Game;
using Kurkku.Util.Extensions;
using System;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    public class UserRemoveComposer : IMessageComposer
    {
        private int instanceId;

        public UserRemoveComposer(int instanceId)
        {
            this.instanceId = instanceId;
        }

        public override void Write()
        {
            m_Data.Add(Convert.ToString(instanceId));
        }
    }
}
