using System;
using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    internal class SearchMessengerComposer : IMessageComposer
    {
        private List<MessengerUser> friends;
        private List<MessengerUser> users;

        public SearchMessengerComposer(List<MessengerUser> friends, List<MessengerUser> users)
        {
            this.friends = friends;
            this.users = users;
        }

        public override void Write()
        {
            m_Data.Add(friends.Count);

            foreach (var user in friends)
                Serialise(user);

            m_Data.Add(users.Count);

            foreach (var user in users)
                Serialise(user);
        }

        private void Serialise(MessengerUser user)
        {
            m_Data.Add(user.PlayerData.Id);
            m_Data.Add(user.PlayerData.Name);
            m_Data.Add(user.PlayerData.Motto);
            m_Data.Add(user.IsOnline);
            m_Data.Add(false);
            m_Data.Add(string.Empty);
            m_Data.Add(0);
            m_Data.Add(user.PlayerData.Figure);
            m_Data.Add(user.PlayerData.LastOnline.ToString("MM-dd-yyyy HH:mm:ss"));
        }
    }
}