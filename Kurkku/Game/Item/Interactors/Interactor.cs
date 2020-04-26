namespace Kurkku.Game
{
    public abstract class Interactor
    {
        protected Item Item { get; }
        protected bool NeedsExtraDataUpdate { get; set; }
        protected string ExtraData { get; set; }
        public virtual ExtraDataType ExtraDataType { get; }

        protected Interactor(Item item)
        {
            Item = item;
            NeedsExtraDataUpdate = true;
        }

        public virtual object GetExtraData() { return Item.Data.ExtraData; }
    }
}
