using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Game;

namespace Kurkku.Messages.Outgoing
{
    class UpdateFloorItemComposer : IMessageComposer
    {
        private Item item;

        public UpdateFloorItemComposer(Item item)
        {
            this.item = item;
        }

        public override void Write()
        {
            FloorItemsComposer.Serialize(this, item);

            m_Data.Add(item.Data.OwnerData.Id);
            m_Data.Add(item.Data.OwnerData.Name);
        }
    }
}
