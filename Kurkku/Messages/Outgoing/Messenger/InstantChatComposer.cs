namespace Kurkku.Messages.Outgoing
{
    public class InstantChatComposer : IMessageComposer
    {
        private int fromId;
        private string message;

        public InstantChatComposer(int fromId, string message)
        {
            this.fromId = fromId;
            this.message = message;
        }
        public override void Write()
        {
            m_Data.Add(fromId);
            m_Data.Add(message);
            m_Data.Add(0);
        }
    }
}