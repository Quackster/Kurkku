using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outoing.Messenger
{
    internal class InitMessengerComposer : MessageComposer
    {
        private List<MessengerCategoryData> categories;
        private List<MessengerUser> friends;

        public override short Header
        {
            get { return OutgoingEvents.InitMessengerComposer; }
        }

        public InitMessengerComposer(List<MessengerCategoryData> categories, List<MessengerUser> friends)
        {
            this.categories = categories;
            this.friends = friends;
        }

        public override void Write()
        {
            m_Data.Add(900);
            m_Data.Add(600);
            m_Data.Add(500);
            m_Data.Add(400);
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
                m_Data.Add(friend.InRoom);
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