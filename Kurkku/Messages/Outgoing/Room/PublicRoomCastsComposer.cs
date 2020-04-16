namespace Kurkku.Messages.Outgoing
{
    class PublicRoomCastsComposer : IMessageComposer
    {
        private int roomId;
        private string ccts;

        public PublicRoomCastsComposer(int roomId, string ccts)
        {
            this.roomId = roomId;
            this.ccts = ccts;
        }

        public override void Write()
        {
            m_Data.Add(roomId);
            m_Data.Add(ccts);
            m_Data.Add(roomId);
        }
    }
}
