using Kurkku.Game;
using Kurkku.Network.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Incoming
{
    class EffectSelectedMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            int effectId = request.ReadInt();

            if (effectId < 0)
                effectId = 0;

            if (effectId != 0 && !player.EffectManager.Effects.ContainsKey(effectId))
                return;

            player.RoomEntity.UseEffect(effectId);
        }
    }
}
