using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kurkku.Game
{
    class PublicItemManager
    {
        #region Fields

        public static readonly PublicItemManager Instance = new PublicItemManager();

        #endregion

        #region Properties

        public List<PublicItem> PublicItems { get; }

        internal List<PublicItem> GetFirstItems()
        {
            return PublicItems;//.Where(x => x.Data.ParentId == 0).ToList();
        }

        #endregion

        #region Constructors

        public PublicItemManager()
        {
            PublicItems = RoomDao.GetPublicItems().Select(x => new PublicItem(x)).ToList();

            foreach (var publicItem in PublicItems)
                AssignChildData(publicItem);
        }


        #endregion

        #region Public methods

        /// <summary>
        /// Assign sub public items
        /// </summary>
        /// <param name="publicItem"></param>
        private void AssignChildData(PublicItem publicItem)
        {
            //publicItem.ChildItems.AddRange(PublicItems.Where(x => x.Data.ParentId == publicItem.Data.BannerId).ToList());
        }


        #endregion
    }
}
