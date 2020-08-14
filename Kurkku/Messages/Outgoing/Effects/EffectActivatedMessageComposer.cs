using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;

namespace Kurkku.Messages.Outgoing
{
    class EffectActivatedMessageComposer : IMessageComposer
    {
        private Effect effect;

        public EffectActivatedMessageComposer(Effect effect)
        {
            this.effect = effect;
        }

        public override void Write()
        {
            m_Data.Add(effect.Id);
            m_Data.Add(effect.Duration);
        }
    }
}
