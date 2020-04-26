using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    class RoomEntryInfoComposer : IMessageComposer
    {
        private RoomData roomData;
        private bool isOwner;

        public RoomEntryInfoComposer(RoomData roomData, bool isOwner)
        {
            this.roomData = roomData;
            this.isOwner = isOwner;
        }

        public override void Write()
        {
            m_Data.Add(roomData.IsPrivateRoom);

            if (roomData.IsPrivateRoom)
            {
                m_Data.Add(roomData.Id);
                m_Data.Add(isOwner);
            }
            else
            {
                m_Data.Add(roomData.Description);
                m_Data.Add(roomData.Id);
            }
        }
    }
}
