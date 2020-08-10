using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class EffectManager : ILoadable
    {
        #region Properties

        public IEntity Entity;
        public ConcurrentDictionary<int, Effect> Effects { get; set; }

        #endregion

        #region Constructor

        public EffectManager(IEntity entity)
        {
            Entity = entity;
        }

        #endregion

        #region Public methods

        public void Load()
        {
            Effects = new ConcurrentDictionary<int, Effect>();

            foreach (var effectData in EffectDao.GetUserEffects(Entity.EntityData.Id))
            {
                Effect effect = new Effect(effectData);
                Effects.TryAdd(effect.Id, effect);
            }
        }

        #endregion

    }
}
