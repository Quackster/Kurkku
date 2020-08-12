using Kurkku.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    class EffectAddedMessageComposer : IMessageComposer
    {
        private Effect effect;

        public EffectAddedMessageComposer(Effect effect)
        {
            this.effect = effect;
        }

        public override void Write()
        {
            EffectsMessageComposer.Compose(effect, this);
        }
    }
}
