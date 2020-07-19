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
        public ConcurrentDictionary<string, QueuedStateData> QueuedStates { get; set; }
        #endregion

        #region Constructor

        protected Interactor(Item item)
        {
            Item = item;
            NeedsExtraDataUpdate = true;
            QueuedStates = new ConcurrentDictionary<string, QueuedStateData>();
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
            foreach (var kvp in QueuedStates.ToArray())
            {
                var key = kvp.Key;
                var queuedStateData = kvp.Value;

                if (queuedStateData.TicksTimer > 0)
                    queuedStateData.TicksTimer--;

                if (queuedStateData.TicksTimer == 0)
                {
                    QueuedStates.Remove(key);
                    ProcessTickState(key, queuedStateData.Attributes);
                }
            }
        }

        /// <summary>
        /// Queue state to process for the future
        /// </summary>
        public void QueueState(string state, double time, Dictionary<object, object> attributes)
        {
            if (QueuedStates.ContainsKey(state))
                QueuedStates.Remove(state);

            QueuedStates.TryAdd(state, new QueuedStateData(RoomTaskManager.GetProcessTime(time), attributes));
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
        public virtual void ProcessTickState(string state, Dictionary<object, object> attributes) { }
        public virtual void OnStop(IEntity entity) { }
        public virtual void OnInteract(IEntity entity) { }
        public virtual void OnPickup(IEntity entity) { }
        public virtual void OnPlace(IEntity entity) { }
        public virtual bool OnWalkRequest(IEntity entity, Position goal) { return false; }

        #endregion
    }

    public class QueuedStateData
    {
        #region Properties

        public int TicksTimer { get; set; } 
        public Dictionary<object, object> Attributes { get; set; }

        #endregion

        #region Constructor

        public QueuedStateData(int ticksTimer, Dictionary<object, object> attributes)
        {
            TicksTimer = ticksTimer;
            Attributes = attributes;
        }

        #endregion
    }
}
