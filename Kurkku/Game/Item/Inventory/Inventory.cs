using Kurkku.Storage.Database.Access;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            this.Items = new ConcurrentDictionary<int, Item>(ItemDao.GetUserItems(player.Details.Id).Select(x => new Item(x)).ToDictionary(x => x.Id, x => x)); 
        }

        #endregion



    }
}
