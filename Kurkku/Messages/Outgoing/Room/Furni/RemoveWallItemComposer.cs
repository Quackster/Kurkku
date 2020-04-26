using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;

namespace Kurkku.Messages.Outgoing
{
    public class RemoveWallItemComposer : IMessageComposer
    {
        private Item item;

        public RemoveWallItemComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            m_Data.Add(item.Id.ToString());
            m_Data.Add(item.Data.OwnerId);
        }
    }
}
