namespace Kurkku.Messages.Outgoing
{
    class UserRightsMessageComposer : IMessageComposer
    {
        private int clubLevel;
        private int rank;

        public UserRightsMessageComposer(int clubLevel, int rank)
        {
            this.clubLevel = clubLevel;
            this.rank = rank;
        }

        public override void Write()
        {
            m_Data.Add(clubLevel);
            m_Data.Add(rank);
        }
    }
}
