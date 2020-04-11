using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    class PublicItem
    {
        #region Properties

        public PublicItemData Data { get; }
        public List<PublicItem> ChildItems { get; }

        #endregion


        public PublicItem(PublicItemData data)
        {
            this.Data = data;
            this.ChildItems = new List<PublicItem>();
        }
    }
}
