namespace Kurkku.Messages.Outgoing
{
    class AllowancesComposer : IMessageComposer
    {
        private bool SAFECHAT;
        private bool ISGUIDE;
        private bool VOTE_IN_COMP;

        public AllowancesComposer()
        {
            SAFECHAT = true;
            ISGUIDE = false;
            VOTE_IN_COMP = false;
        }

        public override void Write()
        {
            m_Data.Add(6);
            m_Data.Add("SAFE_CHAT");
            m_Data.Add(SAFECHAT);
            m_Data.Add((!SAFECHAT) ? "requirement.unfulfilled.safety_quiz_1" : "");
            m_Data.Add("USE_GUIDE_TOOL");
            m_Data.Add(ISGUIDE);
            m_Data.Add((!ISGUIDE) ? "requirement.unfulfilled.helper_level_4" : "");
            m_Data.Add("VOTE_IN_COMPETITIONS");
            m_Data.Add(VOTE_IN_COMP);
            m_Data.Add((!VOTE_IN_COMP) ? "requirement.unfulfilled.helper_level_2" : "");
            m_Data.Add("TRADE");
            m_Data.Add(true);
            m_Data.Add("");
            m_Data.Add("FULL_CHAT");
            m_Data.Add(true);
            m_Data.Add("");
            m_Data.Add("CITIZEN");
            m_Data.Add(true);
            m_Data.Add("");
        }
    }
}
