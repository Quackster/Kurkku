namespace Kurkku.Messages.Outgoing
{
    public class FollowBuddyErrorComposer : IMessageComposer
    {
        private FollowBuddyError followBuddyError;

        public FollowBuddyErrorComposer(FollowBuddyError followBuddyError)
        {
            this.followBuddyError = followBuddyError;
        }

        public override void Write()
        {
            m_Data.Add((int)followBuddyError);
        }
    }

    public enum FollowBuddyError
    {
        NotFriend = 0,
        Offline = 1,
        HotelView = 2,
        Prevented = 3
    }
}