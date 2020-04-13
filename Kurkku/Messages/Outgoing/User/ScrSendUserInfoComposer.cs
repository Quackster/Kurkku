using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing
{
    internal class ScrSendUserInfoComposer : MessageComposer
    {
        public override short Header
        {
            get { return OutgoingEvents.ScrSendUserInfoComposer; }
        }

        public override void Write()
        {
            m_Data.Add("habbo_club"); // Which product/widget to assign the value
            m_Data.Add(1); // DAYS LEFT
            m_Data.Add(0); // unused ??
            m_Data.Add(1); // MONTHS LEFT
            m_Data.Add(0); // unused ??
            m_Data.Add(false); // unused ??
            m_Data.Add(false); // unused ??
            m_Data.Add(0); // unused ??
            m_Data.Add(0); // unused ??
            m_Data.Add(0); // unused ??
        }
    }
}