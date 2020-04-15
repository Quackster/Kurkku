namespace Kurkku.Messages.Outgoing
{
    class RoomForwardComposer : IMessageComposer
    {
        private int roomId;
        private bool isPublicRoom;

        public RoomForwardComposer(int roomId, bool isPublicRoom)
        {
            this.roomId = roomId;
            this.isPublicRoom = isPublicRoom;
        }

        public override void Write()
        {
            m_Data.Add(isPublicRoom);
            m_Data.Add(roomId);
        }
    }
}
