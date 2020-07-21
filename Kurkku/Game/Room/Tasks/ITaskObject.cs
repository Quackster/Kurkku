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
                    ProcessQueuedEvent(queuedStateData);
                }
            }
        }

        /// <summary>
        /// Queue state to process for the future
        /// </summary>
        public void QueueEvent(string state, double time, Dictionary<object, object> attributes)
        {
            if (EventQueue.ContainsKey(state))
                EventQueue.Remove(state);

            EventQueue.TryAdd(state, new QueuedEvent(state, RoomTaskManager.GetProcessTime(time), attributes));
        }

        public virtual void OnTick() { }
        public virtual void OnTickComplete() { }
        public virtual void ProcessQueuedEvent(QueuedEvent queuedEvent) { }
    }
}
