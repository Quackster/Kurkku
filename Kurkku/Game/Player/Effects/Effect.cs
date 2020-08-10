using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class Effect
    {
        #region Properties

        public int Id;
        public EffectData Data { get; set; }

        #endregion

        #region Constructor

        public Effect(EffectData effectData)
        {
            Id = ItemManager.Instance.GenerateEffectId();
            Data = effectData;
        }

        #endregion
    }
}
