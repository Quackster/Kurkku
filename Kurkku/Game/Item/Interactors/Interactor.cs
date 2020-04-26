using Newtonsoft.Json;

namespace Kurkku.Game
{
    public abstract class Interactor
    {
        protected bool NeedsExtraDataUpdate { get; set; }
        protected string ExtraData { get; set; }
        protected string ExtraDataInventoryView { get; set; }
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
            Item.Data.ExtraData = JsonConvert.SerializeObject(jsonObject);
            Item.Save();

            NeedsExtraDataUpdate = true;
        }

        public virtual void OnStop(IEntity entity) { }
    }
}
