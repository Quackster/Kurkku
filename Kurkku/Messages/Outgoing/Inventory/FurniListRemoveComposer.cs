namespace Kurkku.Messages.Outgoing
{
    public class FurniListRemoveComposer : IMessageComposer
    {
        private int itemId;

        public FurniListRemoveComposer(int itemId)
        {
            this.itemId = itemId;
        }

        public override void Write()
        {
            m_Data.Add(itemId);
        }
    }
}
