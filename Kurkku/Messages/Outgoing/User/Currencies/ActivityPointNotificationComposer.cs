namespace Kurkku.Messages.Outgoing
{
    class ActivityPointNotificationComposer : IMessageComposer
    {
        public override void Write()
        {
            m_Data.Add(1);
            m_Data.Add(0);
            m_Data.Add(10);
        }
    }
}
