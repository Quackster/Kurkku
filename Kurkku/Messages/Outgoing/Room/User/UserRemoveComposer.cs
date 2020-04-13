using System;

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
