using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    public enum ItemPlaceError
    {
        NoRights = 1,
        NoPlacementAllowed = 11
    }

    class ItemPlaceErrorComposer : IMessageComposer
    {
        private ItemPlaceError itemPlaceError;

        public ItemPlaceErrorComposer(ItemPlaceError itemPlaceError)
        {
            this.itemPlaceError = itemPlaceError;
        }

        public override void Write()
        {
            m_Data.Add((int)itemPlaceError);
        }
    }
}
