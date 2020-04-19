using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;
using Kurkku.Network.Streams;

namespace Kurkku.Messages.Incoming
{
    public class GetCatalogRoomPromotionMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {
            throw new NotImplementedException();
        }
    }
}
