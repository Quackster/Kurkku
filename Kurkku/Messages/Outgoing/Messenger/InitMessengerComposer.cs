using System.Collections.Generic;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outoing.Messenger
{
    internal class InitMessengerComposer : MessageComposer
    {
        private List<MessengerCategoryData> categories;
        private List<MessengerFriendData> friends;

        public override short Header
        {
            get { return OutgoingEvents.InitMessengerComposer; }
        }

        public InitMessengerComposer(List<MessengerCategoryData> categories, List<MessengerFriendData> friends)
        {
            this.categories = categories;
            this.friends = friends;
        }

        public override void Write()
        {
            this.m_Data.Add(1);
            this.m_Data.Add(1);
            this.m_Data.Add(1);
            this.m_Data.Add(1);
            this.m_Data.Add(this.categories.Count);

            int i = 1;
            foreach (var category in categories)
            {
                this.m_Data.Add(i);
                this.m_Data.Add(category.Label);
                i++;
            }

            this.m_Data.Add(this.friends.Count);
            foreach (var friend in friends)
            {
                this.m_Data.Add(friend.FriendData.Id);
                this.m_Data.Add(friend.FriendData.Name);
                this.m_Data.Add(1);
                this.m_Data.Add(false); // is online
                this.m_Data.Add(false); // is in room
                this.m_Data.Add(friend.FriendData.Figure);
                this.m_Data.Add(1);
                this.m_Data.Add(""); // motto
                this.m_Data.Add(""); // real name
                this.m_Data.Add(""); // unknown??
                this.m_Data.Add(false);
                this.m_Data.Add(false);
                this.m_Data.Add(false);
                this.m_Data.Add((short)0); // relationship status
            }
        }
    }
}