using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming
{
    class EffectActivatedMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int effectId = request.ReadInt();

            if (!player.EffectManager.Effects.ContainsKey(effectId))
                return;

            var effect = player.EffectManager.Effects[effectId];

            if (!effect.TryActivate())
                return;

            player.Send(new EffectActivatedMessageComposer(effect));
        }
    }
}
