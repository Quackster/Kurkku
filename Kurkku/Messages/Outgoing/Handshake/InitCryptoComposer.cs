namespace Kurkku.Messages.Outgoing
{
    public class InitCryptoComposer : IMessageComposer
    {
        private string secretKey;

        public InitCryptoComposer(string secretKey)
        {
            this.secretKey = secretKey;
        }

        public override void Write()
        {
            m_Data.Add(false);
            m_Data.Add(false);
            m_Data.Add(this.secretKey);
        }
    }
}
