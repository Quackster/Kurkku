using Kurkku.Util.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public abstract class ITaskObject
    {
        #region Properties 

        protected long TicksTimer { get; set; }
        public virtual bool RequiresTick { get; }
        public ConcurrentDictionary<string, QueuedEvent> EventQueue { get; set; }
        public Item Item { get; set; }
        public IEntity Entity { get; set; }

        #endregion

        #region Constructor

        protected ITaskObject(Item item)
        {
            Item = item;
            EventQueue = new ConcurrentDictionary<string, QueuedEvent>();
        }

        protected ITaskObject(IEntity entity)
        {
            Entity = entity;
            EventQueue = new ConcurrentDictionary<string, QueuedEvent>();
        }

        #endregion

        protected void CancelTicks()
        {
            TicksTimer = -1;
        }

        public bool CanTick()
        {
            if (TicksTimer > 0)
                TicksTimer--;
            
            if (TicksTimer == 0)
            {
                CancelTicks();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try process a future state
        /// </summary>
        public void TryTickState()
        {
            foreach (var kvp in EventQueue.ToArray())
            {
                var key = kvp.Key;
                var queuedStateData = kvp.Value;

                if (queuedStateData.TicksTimer > 0)
                    queuedStateData.TicksTimer--;

                if (queuedStateData.TicksTimer == 0)
                {
                    EventQueue.Remove(key);
                    queuedStateData.Action(queuedStateData);
                }
            }
        }

        /// <summary>
        /// Queue state to process for the future
        /// </summary>
        public void QueueEvent(string state, double time, Action<QueuedEvent> action, Dictionary<object, object> attributes = null)
        {
            if (EventQueue.ContainsKey(state))
                EventQueue.Remove(state);

            EventQueue.TryAdd(state, new QueuedEvent(state, action, RoomTaskManager.GetProcessTime(time), attributes));
        }

        internal void QueueEvent(string rOLL_DICE, object rolledDice, double v, Dictionary<object, object> dictionary)
        {
            throw new NotImplementedException();
        }

        public virtual void OnTick() { }
        public virtual void OnTickComplete() { }
    }
}
