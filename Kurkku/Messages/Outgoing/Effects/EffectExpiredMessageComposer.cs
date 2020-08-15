using Kurkku.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class EffectExpiredMessageComposer : IMessageComposer
    {
        private int effectId;

        public EffectExpiredMessageComposer(int effectId)
        {
            this.effectId = effectId;
        }

        public override void Write()
        {
            m_Data.Add(effectId);
        }
    }
}
