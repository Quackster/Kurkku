using Kurkku.Storage.Database.Access;
using Kurkku.Util.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Kurkku.Game
{
    public class Inventory : ILoadable
    {
        #region Fields

        private Player player;

        #endregion

        #region Properties

        public ConcurrentDictionary<int, Item> Items;

        #endregion

        #region Constructors

        public Inventory(Player player)
        {
            this.player = player;
        }

        public void Load()
        {
            Items = new ConcurrentDictionary<int, Item>(ItemDao.GetUserItems(player.Details.Id).Select(x => new Item(x)).ToDictionary(x => x.Id, x => x));
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Retrieve item from inventory
        /// </summary>
        public Item GetItem(int itemId)
        {
            if (Items.TryGetValue(itemId, out var item))
                return item;

            return null;
        }

        /// <summary>
        /// Add item to inventory
        /// </summary>
        public void AddItem(Item item, bool alertNewItem = false, bool forceUpdate = false)
        {
            this.Items.TryAdd(item.Id, item);
        }

        /// <summary>
        /// Remove item from inventory
        /// </summary>
        public void RemoveItem(Item item)
        {
            Items.Remove(item.Id);
        }

        #endregion
    }
}
