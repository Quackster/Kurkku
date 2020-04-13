using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing
{
    public class SecretKeyComposer : IMessageComposer
    {
        private string publicKey;

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
