using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outoing.Messenger
{
    internal class UpdateMessengerComposer : MessageComposer
    {
        private List<MessengerCategoryData> categories;
        private List<MessengerUpdate> updates;

        public override short Header
        {
            get { return OutgoingEvents.UpdateMessengerComposer; }
        }

        public UpdateMessengerComposer(List<MessengerCategoryData> categories, List<MessengerUpdate> updates)
        {
            this.categories = categories;
            this.updates = updates;
        }

        public override void Write()
        {
            this.m_Data.Add(this.categories.Count);

            int i = 1;
            foreach (var category in categories)
            {
                this.m_Data.Add(i);
                this.m_Data.Add(category.Label);
                i++;
            }

            this.m_Data.Add(this.updates.Count);
            foreach (var messengerUpdate in updates)
            {
                this.m_Data.Add((int)messengerUpdate.UpdateType);
                this.m_Data.Add(messengerUpdate.Friend.PlayerData.Id);

                if (messengerUpdate.UpdateType == MessengerUpdateType.AddFriend ||
                    messengerUpdate.UpdateType == MessengerUpdateType.UpdateFriend)
                {
                    this.m_Data.Add(messengerUpdate.Friend.PlayerData.Name);
                    this.m_Data.Add(1);
                    this.m_Data.Add(messengerUpdate.Friend.IsOnline);
                    this.m_Data.Add(messengerUpdate.Friend.InRoom);
                    this.m_Data.Add(messengerUpdate.Friend.PlayerData.Figure);
                    this.m_Data.Add(0); // category id
                    this.m_Data.Add(messengerUpdate.Friend.PlayerData.Motto); // motto
                    this.m_Data.Add(messengerUpdate.Friend.PlayerData.RealName); // real name
                    this.m_Data.Add(messengerUpdate.Friend.PlayerData.LastOnline.ToString("MM-dd-yyyy HH:mm:ss")); // unknown??
                    this.m_Data.Add(false);
                    this.m_Data.Add(false);
                    this.m_Data.Add(false);
                    this.m_Data.Add((short)0); // relationship status
                }
            }
        }
    }
}