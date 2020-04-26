using Kurkku.Game;
using Kurkku.Storage.Database.Data;
using Kurkku.Util.Extensions;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;

namespace Kurkku.Messages.Outgoing
{
    public class WallItemComposer : IMessageComposer
    {
        private Item item;

        public WallItemComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            WallItemsComposer.Serialize(this, item);

            m_Data.Add(item.Data.OwnerData.Id);
            m_Data.Add(item.Data.OwnerData.Name);
        }
    }
}
