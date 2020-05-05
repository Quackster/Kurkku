using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    internal class ScrSendUserInfoComposer : IMessageComposer
    {
        private SubscriptionData subscription;

        public ScrSendUserInfoComposer(SubscriptionData subscription)
        {
            this.subscription = subscription;
        }

        public override void Write()
        {
            m_Data.Add("habbo_club"); // Which product/widget to assign the value
            m_Data.Add(subscription != null ? subscription.DaysLeft : 0); // DAYS LEFT
            m_Data.Add(0); // unused ??
            m_Data.Add(subscription != null ? subscription.MonthsLeft : 0); // MONTHS LEFT
            m_Data.Add(0); // unused ??
            m_Data.Add(false); // unused ??
            m_Data.Add(subscription != null);
            m_Data.Add(0); // unused ??
            m_Data.Add(0); // unused ??
            m_Data.Add(0); // unused ??
        }
    }
}