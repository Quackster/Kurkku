using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing
{
    public class SecretKeyComposer : MessageComposer
    {
        private string publicKey;

        public override short Header
        {
            get { return OutgoingEvents.SecretKeyComposer; }
        }

        public SecretKeyComposer(string publicKey)
        {
            this.publicKey = publicKey;
        }

        public override void Write()
        {
            m_Data.Add(this.publicKey);
        }
    }
}
