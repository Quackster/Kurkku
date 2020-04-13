using System.Collections.Generic;
using Kurkku.Game;
using Kurkku.Messages.Headers;

namespace Kurkku.Messages.Outgoing
{
    public class MessengerRequestsComposer : IMessageComposer
    {
        private List<MessengerUser> requests;
        public MessengerRequestsComposer(List<MessengerUser> requests)
        {
            this.requests = requests;
        }

        public override void Write()
        {
            m_Data.Add(requests.Count);
            m_Data.Add(requests.Count);

            foreach (var request in requests)
            {
                m_Data.Add(request.PlayerData.Id);
                m_Data.Add(request.PlayerData.Name);
                m_Data.Add(request.PlayerData.Figure);
            }
        }
    }
}