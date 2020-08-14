using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System;

namespace Kurkku.Game
{
    public class Effect
    {
        #region Properties

        public int Id => Data.EffectId;
        public EffectData Data { get; set; }
        private Player player { get; }
        public int Duration => CatalogueManager.Instance.GetEffectSetting(Id).Duration;
        public bool IsCostume => CatalogueManager.Instance.GetEffectSetting(Id).IsCostume;

        public int TimeLeft
        {
            get
            {
                if (Data.ExpiresAt.HasValue)
                {
                    int seconds = (int)(Data.ExpiresAt - DateTime.Now).Value.TotalSeconds;
                    return seconds >= 0 ? seconds : 0;
                }

                return 0;
            }
        }

        #endregion

        #region Constructor

        public Effect(EffectData effectData)
        {
            this.player = player;
            this.Data = effectData;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Try activate the effects
        /// </summary>
        public bool TryActivate()
        {
            if (Data.IsActivated)
                return false;

            Data.IsActivated = true;
            Data.ExpiresAt = DateTime.Now.AddSeconds(Duration);
            EffectDao.UpdateEffect(Data);

            return true;
        }

        #endregion
    }
}
