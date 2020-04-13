namespace Kurkku.Messages.Outgoing
{
    public class RoomReadyComposer : IMessageComposer
    {
        private string modelName;
        private int roomId;

        public RoomReadyComposer(string modelName, int roomId)
        {
            this.modelName = modelName;
            this.roomId = roomId;
        }

        public override void Write()
        {
            m_Data.Add(modelName);
            m_Data.Add(roomId);
        }
    }
}
