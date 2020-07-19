using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public abstract class Interactor
    {
        #region Properties 

        protected int TicksTimer { get; set; }
        protected bool NeedsExtraDataUpdate { get; set; }
        protected string ExtraData { get; set; }
        public Item Item { get; }
        public virtual ExtraDataType ExtraDataType { get; }
        public virtual bool RequiresTick { get; }
        public ConcurrentDictionary<string, QueuedEvent> EventQueue { get; set; }
        #endregion

        #region Constructor

        protected Interactor(Item item)
        {
            Item = item;
            NeedsExtraDataUpdate = true;
            EventQueue = new ConcurrentDictionary<string, QueuedEvent>();
        }

        #endregion

        #region Tick methods

        protected void CancelTicks()
        {
            TicksTimer = -1;
        }

        public bool CanTick()
        {
            TryTickState();

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

        #endregion

        #region Public methods

        public void SetJsonObject(object jsonObject)
        {
            if (jsonObject is string)
                Item.Data.ExtraData = jsonObject.ToString();
            else
                Item.Data.ExtraData = JsonConvert.SerializeObject(jsonObject);

            NeedsExtraDataUpdate = true;
        }

        public virtual object GetExtraData(bool inventoryView = false) { return Item.Data.ExtraData; }
        public virtual object GetJsonObject() { return null; }
        public virtual void OnTick() { }
        public virtual void OnTickComplete() { }
        public virtual void ProcessQueuedEvent(QueuedEvent queuedEvent) { }
        public virtual void OnStop(IEntity entity) { }
        public virtual void OnInteract(IEntity entity) { }
        public virtual void OnPickup(IEntity entity) { EventQueue.Clear(); }
        public virtual void OnPlace(IEntity entity) { EventQueue.Clear(); }
        public virtual bool OnWalkRequest(IEntity entity, Position goal) { return false; }

        #endregion
    }

    public class QueuedEvent
    {
        #region Properties

        public string EventName { get; set; }
        public int TicksTimer { get; set; } 
        private Dictionary<object, object> Attributes { get; set; }

        #endregion

        #region Constructor

        public QueuedEvent(string eventName, int ticksTimer, Dictionary<object, object> attributes)
        {
            EventName = eventName;
            TicksTimer = ticksTimer;
            Attributes = attributes;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Has an attribute
        /// </summary>
        public bool HasAttribute(object key)
        {
            return Attributes.ContainsKey(key);
        }

        /// <summary>
        /// Get attribute by class it expects
        /// </summary>
        public T GetAttribute<T>(object key)
        {
            if (Attributes.ContainsKey(key))
            {
                return (T)Attributes[key];
            }

            return default(T);
        }

        #endregion
    }
}
