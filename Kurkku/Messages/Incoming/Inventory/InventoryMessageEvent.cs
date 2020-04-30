using Kurkku.Game;
using Kurkku.Messages.Outgoing;
using Kurkku.Network.Streams;
using Kurkku.Util.Extensions;
using System;
using System.Collections.Generic;

namespace Kurkku.Messages.Incoming
{
    class InventoryMessageEvent : IMessageEvent
    {
        public void Handle(Player player, Request request)
        {

            int itemsPerPage = 1000;
            int pages = player.Inventory.Items.Values.CountPages(itemsPerPage);


            for (int i = 0; i < pages; i++)
            {
                List<Item> items = player.Inventory.Items.Values.GetPage(i, itemsPerPage);
                player.Send(new InventoryMessageComposer(pages, i, items));
            }
        }
    }
}
