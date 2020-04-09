using System;
using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    internal class SearchMessengerComposer : MessageComposer
    {
        private List<MessengerUser> friends;
        private List<MessengerUser> users;

        public SearchMessengerComposer(List<MessengerUser> friends, List<MessengerUser> users)
        {
            this.friends = friends;
            this.users = users;
        }

        public override short Header
        {
            get { return OutgoingEvents.SearchMessengerComposer; }
        }

        public override void Write()
        {
            /*
             *             reply.AppendInt32(this.userID);
             reply.AppendString(this.username);
             reply.AppendString(this.motto);
             bool b = SwiftEmuEnvironment.GetGame().GetClientManager().GetClient(this.userID) != null;
             reply.AppendBoolean(b);
             reply.AppendBoolean(false);
             reply.AppendString(string.Empty);
             reply.AppendInt32(0);
             reply.AppendString(this.look);
             reply.AppendString(this.last_online);*/
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