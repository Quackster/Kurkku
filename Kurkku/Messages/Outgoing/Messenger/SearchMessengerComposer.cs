using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outoing
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
           
        }
    }
}