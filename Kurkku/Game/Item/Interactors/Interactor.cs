using Newtonsoft.Json;

namespace Kurkku.Game
{
    public abstract class Interactor
    {
        protected bool NeedsExtraDataUpdate { get; set; }
        protected string ExtraData { get; set; }
        public Item Item { get; }
        public virtual ExtraDataType ExtraDataType { get; }

        protected Interactor(Item item)
        {
            Item = item;
            NeedsExtraDataUpdate = true;
        }

        public virtual object GetExtraData(bool inventoryView = false) { return Item.Data.ExtraData; }
        public virtual object GetJsonObject() { return null; }
        public void SetJsonObject(object jsonObject)
        {
            if (jsonObject is string)
                Item.Data.ExtraData = jsonObject.ToString();
            else
                Item.Data.ExtraData = JsonConvert.SerializeObject(jsonObject);

            NeedsExtraDataUpdate = true;
        }

        public virtual void OnStop(IEntity entity) { }
        public virtual void OnInteract(IEntity entity) { }
        public virtual bool OnWalkRequest(IEntity entity, Position goal) { return false; }
    }
}
