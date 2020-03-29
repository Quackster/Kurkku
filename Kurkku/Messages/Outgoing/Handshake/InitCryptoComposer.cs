using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing.Handshake
{
    public class InitCryptoComposer : MessageComposer
    {
        private string secretKey;

        public override short Header
        {
            get { return OutgoingEvents.InitCryptoComposer; }
        }

        public InitCryptoComposer(string secretKey)
        {
            this.secretKey = secretKey;
        }

        public override void Write()
        {
            m_Data.Add(this.secretKey);
            m_Data.Add(false);
        }
    }
}
