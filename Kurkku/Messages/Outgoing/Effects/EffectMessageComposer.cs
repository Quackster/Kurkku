using Kurkku.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class EffectMessageComposer : IMessageComposer
    {
        private int instanceId;
        private int effectId;

        public EffectMessageComposer(int instanceId, int effectId)
        {
            this.instanceId = instanceId;
            this.effectId = effectId;
        }

        public override void Write()
        {
            m_Data.Add(instanceId);
            m_Data.Add(effectId);
            m_Data.Add(0);
        }
    }
}
