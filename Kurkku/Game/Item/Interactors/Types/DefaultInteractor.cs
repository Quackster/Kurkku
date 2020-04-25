using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class DefaultInteractor : Interactor
    {
        public DefaultInteractor(Item item) : base(item)
        {

        }

        public override string GetExtraData()
        {
            return Item.Data.ExtraData;
        }
    }
}
