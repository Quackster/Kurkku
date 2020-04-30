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
            var inventoryItems = new List<Item>(player.Inventory.Items.Values);

            int itemsPerPage = ValueManager.Instance.GetInt("inventory.items.per.page");
            int pages = inventoryItems.CountPages(itemsPerPage);

            for (int i = 0; i < pages; i++)
            {
                List<Item> items = inventoryItems.GetPage(i, itemsPerPage);
                player.Send(new InventoryMessageComposer(pages, i, items));
            }
        }
    }
}
