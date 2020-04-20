namespace Kurkku.Messages.Outgoing
{
    public class TypingStatusComposer : IMessageComposer
    {
        private int instanceId;
        private bool isTyping;

        public TypingStatusComposer(int instanceId, bool isTyping)
        {
            this.instanceId = instanceId;
            this.isTyping = isTyping;
        }

        public override void Write()
        {
            m_Data.Add(instanceId);
            m_Data.Add(isTyping ? 1 : 0);
        }
    }
}
