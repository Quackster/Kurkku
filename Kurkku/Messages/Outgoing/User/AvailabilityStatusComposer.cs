namespace Kurkku.Messages.Outgoing
{
    class AvailabilityStatusComposer : IMessageComposer
    {
        private bool isOpen;
        private bool isTradingEnded;

        public AvailabilityStatusComposer()
        {
            this.isOpen = true;
            this.isTradingEnded = false;
        }

        public override void Write()
        {
            m_Data.Add(isOpen);
            m_Data.Add(isTradingEnded);
        }
    }
}
