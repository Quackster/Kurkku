using System;
using System.Collections.Generic;
using System.Text;
using static Kurkku.Game.DiceInteractor;

namespace Kurkku.Game
{
    public class DiceTaskObject : ITaskObject
    {
        #region Constructor

        public DiceTaskObject(Item item) : base(item) { }

        #endregion

        #region Public methods

        public override void OnTick() { }
        public override void OnTickComplete() { }

        /// <summary>
        /// Process future state
        /// </summary>
        /// 
        public override void ProcessQueuedEvent(QueuedEvent queuedEvent)
        {
            if (!queuedEvent.HasAttribute(DiceAttributes.ENTITY))
                return;

            var entity = queuedEvent.GetAttribute<IEntity>(DiceAttributes.ENTITY);

            switch (queuedEvent.EventName)
            {
                case DiceAttributes.ROLL_DICE:
                    {
                        Item.UpdateStatus("3");
                        Item.Save();
                    }
                    break;
            }
        }

        #endregion
    }
}
