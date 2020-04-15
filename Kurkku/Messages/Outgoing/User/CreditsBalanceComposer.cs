namespace Kurkku.Messages.Outgoing
{
    public class CreditsBalanceComposer : IMessageComposer
    {
        private int credits;

        public CreditsBalanceComposer(int credits)
        {
            this.credits = credits;
        }

        public override void Write()
        {
            m_Data.Add($"{credits}.0");
        }
    }
}
