using Kurkku.Messages.Outgoing;
using Kurkku.Storage.Database.Access;
using Kurkku.Util;
using Kurkku.Util.Extensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kurkku.Game
{
    public class PlayerTaskObject : ITaskObject
    {
        private class PlayerAttribute
        {
            public const string TYPING_STATUS = "TYPING_STATUS";
            public const string EFFECT_EXPIRY = "EFFECT_EXPIRY";
        }

        #region Fields

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructor

        public PlayerTaskObject(IEntity entity) : base(entity) { }

        #endregion

        #region Override Properties

        public override bool RequiresTick => true;

        #endregion

        #region Public methods

        public override void OnTick() { }
        public override void OnTickComplete()
        {
            if (!EventQueue.ContainsKey(PlayerAttribute.TYPING_STATUS))
                QueueEvent(PlayerAttribute.TYPING_STATUS, 1.0, ProcessTypingStatus, null);

            if (!EventQueue.ContainsKey(PlayerAttribute.EFFECT_EXPIRY))
                QueueEvent(PlayerAttribute.EFFECT_EXPIRY, 1.0, ProcessEffectExpiry, null);

            TicksTimer = RoomTaskManager.GetProcessTime(0.5);
        }

        /// <summary>
        /// Process typing status stopped after x seconds
        /// </summary>
        public void ProcessTypingStatus(QueuedEvent queuedEvent)
        {
            if (!(Entity is Player))
                return;

            var player = (Player)Entity;

            if (player.RoomUser.TimerManager.SpeechBubbleDate != -1 && DateUtil.GetUnixTimestamp() > player.RoomUser.TimerManager.SpeechBubbleDate)
            {
                player.RoomUser.TimerManager.ResetSpeechBubbleTimer();
                player.RoomUser.Room.Send(new TypingStatusComposer(player.RoomUser.InstanceId, false));
            }
        }

        /// <summary>
        /// Process effect expiry
        /// </summary>
        public void ProcessEffectExpiry(QueuedEvent queuedEvent)
        {
            if (!(Entity is Player))
                return;

            var player = (Player)Entity;

            foreach (var effect in player.EffectManager.Effects.Where(x => x.Value.Data.IsActivated && x.Value.Data.ExpiresAt != null && DateTime.Now > x.Value.Data.ExpiresAt).ToList())
            {
                if (effect.Value.Data.Quantity > 0)
                    effect.Value.Data.Quantity--;

                effect.Value.Data.ExpiresAt = null;

                if (effect.Value.Data.Quantity == 0)
                {
                    player.EffectManager.Effects.Remove(effect.Value.Id);
                    EffectDao.DeleteEffect(effect.Value.Data);
                }
                else
                {
                    EffectDao.UpdateEffect(effect.Value.Data);
                }

                player.Send(new EffectExpiredMessageComposer(effect.Value.Id));

                if (player.RoomEntity.EffectId == effect.Value.Id)
                    player.RoomEntity.UseEffect(0);
            }
        }

        #endregion
    }
}
