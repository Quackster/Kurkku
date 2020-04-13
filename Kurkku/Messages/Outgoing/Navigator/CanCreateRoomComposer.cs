using Kurkku.Messages;

namespace Kurkku
{
    public class CanCreateRoomComposer : IMessageComposer
    {
        private bool hasReachedLimit;
        private int maxRooms;

        public CanCreateRoomComposer(bool hasReachedLimit, int maxRooms)
        {
            this.hasReachedLimit = hasReachedLimit;
            this.maxRooms = maxRooms;
        }

        public override void Write()
        {
            m_Data.Add(hasReachedLimit ? 0 : 1);
            m_Data.Add(maxRooms);
        }
    }
}