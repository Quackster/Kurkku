using Newtonsoft.Json;

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

        #endregion

        #region Constructor

        protected Interactor(Item item)
        {
            Item = item;
            NeedsExtraDataUpdate = true;
        }

        #endregion

        #region Tick methods

        public bool RequiresTick()
        {
            return Item.Definition.InteractorType == InteractorType.ROLLER;
        }

        public bool HasTicks()
        {
            return (TicksTimer > 0);
        }

        public void SetTicks(int time)
        {
            TicksTimer = time;
        }

        protected void CancelTicks()
        {
            TicksTimer = -1;
        }

        public bool CanTick()
        {
            //OnTick();

            if (TicksTimer > 0)
                TicksTimer--;

            if (TicksTimer == 0)
            {
                CancelTicks();
                return true;
            }

            return false;
            /*if (TicksTimer == 0)
            {
                CancelTicks();
                OnTickComplete();
            }*/
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
        public virtual void OnStop(IEntity entity) { }
        public virtual void OnInteract(IEntity entity) { }
        public virtual void OnPickup(IEntity entity) { }
        public virtual void OnPlace(IEntity entity) { }
        public virtual bool OnWalkRequest(IEntity entity, Position goal) { return false; }

        #endregion
    }
}
