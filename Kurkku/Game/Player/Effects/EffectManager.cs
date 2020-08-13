using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class EffectManager : ILoadable
    {
        #region Properties

        private Player player;
        public ConcurrentDictionary<int, Effect> Effects { get; set; }

        #endregion

        #region Constructor

        public EffectManager(Player player)
        {
            this.player = player;
        }

        #endregion

        #region Public methods

        public void Load()
        {
            Effects = new ConcurrentDictionary<int, Effect>();

            foreach (var effectData in EffectDao.GetUserEffects(player.EntityData.Id))
            {
                Effect effect = new Effect(effectData);
                Effects.TryAdd(effect.Id, effect);
            }

            player.Send(new EffectsMessageComposer(new List<Effect>(Effects.Values)));
        }

        /// <summary>
        /// Add effect to collection
        /// </summary>
        public void AddEffect(EffectData effectData)
        {
            var effect = new Effect(effectData);

            player.Send(new EffectAddedMessageComposer(effect));
            Effects.TryAdd(effect.Id, effect);
        }

        #endregion
    }
}
