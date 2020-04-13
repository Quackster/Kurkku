using Kurkku.Messages;
using Kurkku.Messages.Headers;

namespace Kurkku
{
    public class CanCreateRoomComposer : MessageComposer
    {
        private bool hasReachedLimit;
        private int maxRooms;

        public override short Header
        {
            get { return OutgoingEvents.CanCreateRoomComposer; }
        }

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