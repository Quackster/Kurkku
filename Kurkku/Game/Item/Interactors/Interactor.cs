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

        protected bool NeedsExtraDataUpdate { get; set; }
        protected object ExtraData { get; set; }
        public Item Item { get; }
        public virtual ExtraDataType ExtraDataType { get; }
        public virtual ITaskObject TaskObject { get; set; }

        #endregion

        #region Constructor

        protected Interactor(Item item)
        {
            Item = item;
            NeedsExtraDataUpdate = true;
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
        public virtual void OnStop(IEntity entity) { }
        public virtual void OnInteract(IEntity entity, int requestData = 0) { }
        public virtual void OnPickup(IEntity entity) { TaskObject?.EventQueue.Clear(); }
        public virtual void OnPlace(IEntity entity) { TaskObject?.EventQueue.Clear(); }
        public virtual bool OnWalkRequest(IEntity entity, Position goal) { return false; }

        #endregion
    }

    public class QueuedEvent
    {
        #region Properties

        public string EventName { get; set; }
        public long TicksTimer { get; set; } 
        public Action<QueuedEvent> Action { get; set; }
        private Dictionary<object, object> Attributes { get; set; }

        #endregion

        #region Constructor

        public QueuedEvent(string eventName, Action<QueuedEvent> action, long ticksTimer, Dictionary<object, object> attributes)
        {
            EventName = eventName;
            Action = action;
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
