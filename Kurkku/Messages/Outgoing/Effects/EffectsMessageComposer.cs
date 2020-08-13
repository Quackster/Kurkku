using Kurkku.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class EffectsMessageComposer : IMessageComposer
    {
        private List<Effect> effects;

        public EffectsMessageComposer(List<Effect> effects)
        {
            this.effects = effects;
        }

        public override void Write()
        {
            m_Data.Add(effects.Count);
        
            foreach (var effect in effects)
            {
                Compose(effect, this);
            }
        }

        internal static void Compose(Effect effect, IMessageComposer composer)
        {
            composer.Data.Add(effect.Id);
            composer.Data.Add(effect.IsCostume ? 1 : 0);
            composer.Data.Add(effect.Duration);
            composer.Data.Add(effect.Data.IsActivated ? effect.Data.Quantity : effect.Data.Quantity);
            composer.Data.Add(effect.Data.IsActivated ? effect.TimeLeft : -1);
        }
    }
}
