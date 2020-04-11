using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Messages.Incoming
{
    class PublicItemsMessengerEvent : MessageEvent
    {
        public void Handle(Player player, Request request)
        {
            player.Send(new PublicItemsComposer(PublicItemManager.Instance.GetFirstItems()));
        }
    }
}
