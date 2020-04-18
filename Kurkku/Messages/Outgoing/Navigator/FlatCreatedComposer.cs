namespace Kurkku.Messages.Outgoing
{
    class FlatCreatedComposer : IMessageComposer
    {
        private int roomId;
        private string roomName;

        public FlatCreatedComposer(int roomId, string roomName)
        {
            this.roomId = roomId;
            this.roomName = roomName;
        }

        public override void Write()
        {
            m_Data.Add(roomId);
            m_Data.Add(roomName);
        }
    }
}
