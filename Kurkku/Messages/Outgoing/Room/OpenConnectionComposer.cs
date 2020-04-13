namespace Kurkku.Messages.Outgoing
{
    class OpenConnectionComposer : IMessageComposer
    {
        private int roomId;
        private int categoryId;

        public OpenConnectionComposer(int roomId, int categoryId)
        {
            this.roomId = roomId;
            this.categoryId = categoryId;
        }

        public override void Write()
        {
            m_Data.Add(roomId);
            m_Data.Add(categoryId);
        }
    }
}
