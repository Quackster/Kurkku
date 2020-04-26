namespace Kurkku.Messages.Outgoing
{
    public class ChatMessageComposer : IMessageComposer
    {
        private int instanceId;
        private string message;
        private int colour;
        private int gesture;

        public ChatMessageComposer(int instanceId, string message, int colour, int gesture)
        {
            this.instanceId = instanceId;
            this.message = message;
            this.colour = colour;
            this.gesture = gesture;
        }

        public override void Write()
        {
            m_Data.Add(instanceId);
            m_Data.Add(message);
            m_Data.Add(gesture);
            m_Data.Add(colour);
            m_Data.Add(0);
            m_Data.Add(-1);
        }
    }
}
