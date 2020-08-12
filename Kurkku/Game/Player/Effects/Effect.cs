using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class Effect
    {
        #region Properties

        public int Id => Data.EffectId;
        public EffectData Data { get; set; }
        private Player player { get; }
        public int Duration => player.EffectManager.GetEffectDuration(Id);

        public int TimeLeft
        {
            get
            {
                if (Data.ExpireAt.HasValue)
                {
                    int seconds = (int)(Data.ExpireAt - DateTime.Now).Value.TotalSeconds;
                    return seconds >= 0 ? seconds : 0;
                }

                return 0;
            }
        }

        #endregion

        #region Constructor

        public Effect(Player player, EffectData effectData)
        {
            this.player = player;
            this.Data = effectData;
        }

        #endregion
    }
}
