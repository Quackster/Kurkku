namespace Kurkku.Game
{
    public abstract class Interactor
    {
        protected Item Item { get; }

        protected Interactor(Item item)
        {
            Item = item;
        }

        public abstract string GetExtraData();
    }
}
