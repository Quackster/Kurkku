using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    internal class InitMessengerComposer : MessageComposer
    {
        private int maxNormalFriends;
        private int maxHCFriends;
        private int maxVIPFriends;

        private List<MessengerCategoryData> categories;
        private List<MessengerUser> friends;

        public override short Header
        {
            get { return OutgoingEvents.InitMessengerComposer; }
        }

        public InitMessengerComposer(int maxNormalFriends, int maxHCFriends, int maxVIPFriends, List<MessengerCategoryData> categories, List<MessengerUser> friends)
        {
            this.maxNormalFriends = maxNormalFriends;
            this.maxHCFriends = maxHCFriends;
            this.maxVIPFriends = maxVIPFriends;

            this.categories = categories;
            this.friends = friends;
        }

        public override void Write()
        {
            // See https://habbo.fandom.com/wiki/Benefits_of_VIP
            m_Data.Add(maxNormalFriends); // HC            _SafeStr_10476():int
            m_Data.Add(0);
            m_Data.Add(maxHCFriends); // HC limit         _SafeStr_10477():int
            m_Data.Add(maxVIPFriends); // VIP limit        _local_2._SafeStr_10478
            m_Data.Add(categories.Count);

            int i = 1;
            foreach (var category in categories)
            {
                m_Data.Add(i);
                m_Data.Add(category.Label);
                i++;
            }

            m_Data.Add(friends.Count);
            foreach (var friend in friends)
            {
                m_Data.Add(friend.PlayerData.Id);
                m_Data.Add(friend.PlayerData.Name);
                m_Data.Add(1);
                m_Data.Add(friend.IsOnline);
                m_Data.Add(friend.Player != null ? (friend.Player.Settings.FollowingEnabled && friend.InRoom) : false);
                m_Data.Add(friend.PlayerData.Figure);
                m_Data.Add(0); // category id
                m_Data.Add(friend.PlayerData.Motto); // motto
                m_Data.Add(friend.PlayerData.RealName); // real name
                m_Data.Add(friend.PlayerData.LastOnline.ToString("MM-dd-yyyy HH:mm:ss")); // unknown??
                m_Data.Add(false);
                m_Data.Add(false);
                m_Data.Add(false);
                m_Data.Add((short)0); // relationship status
            }
        }
    }
}