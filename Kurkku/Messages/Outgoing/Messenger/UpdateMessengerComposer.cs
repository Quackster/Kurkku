using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    internal class UpdateMessengerComposer : IMessageComposer
    {
        private List<MessengerCategoryData> categories;
        private List<MessengerUpdate> updates;

        public UpdateMessengerComposer(List<MessengerCategoryData> categories, List<MessengerUpdate> updates)
        {
            this.categories = categories;
            this.updates = updates;
        }

        public override void Write()
        {
            m_Data.Add(categories.Count);

            int i = 1;
            foreach (var category in categories)
            {
                m_Data.Add(i);
                m_Data.Add(category.Label);
                i++;
            }

            m_Data.Add(updates.Count);
            foreach (var messengerUpdate in updates)
            {
                m_Data.Add((int)messengerUpdate.UpdateType);

                switch(messengerUpdate.UpdateType)
                {
                    case MessengerUpdateType.RemoveFriend:
                        {
                            m_Data.Add(messengerUpdate.Friend.PlayerData.Id);
                            break;
                        }
                    case MessengerUpdateType.AddFriend:
                    case MessengerUpdateType.UpdateFriend:
                        {
                            m_Data.Add(messengerUpdate.Friend.PlayerData.Id);
                            m_Data.Add(messengerUpdate.Friend.PlayerData.Name);
                            m_Data.Add(1);
                            m_Data.Add(messengerUpdate.Friend.IsOnline);
                            m_Data.Add(messengerUpdate.Friend.InRoom);
                            m_Data.Add(messengerUpdate.Friend.IsOnline ? messengerUpdate.Friend.PlayerData.Figure : "");
                            m_Data.Add(0); // category id
                            m_Data.Add(messengerUpdate.Friend.PlayerData.Motto); // motto
                            m_Data.Add(messengerUpdate.Friend.PlayerData.RealName); // real name
                            m_Data.Add(messengerUpdate.Friend.PlayerData.LastOnline.ToString("MM-dd-yyyy HH:mm:ss")); // unknown??
                            m_Data.Add(false);
                            m_Data.Add(false);
                            m_Data.Add(false);
                            m_Data.Add((short)0); // relationship status
                            break;
                        }
                }
            }
        }
    }
}