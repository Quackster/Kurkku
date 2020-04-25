using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class StickieInteractor : Interactor
    {
        public StickieInteractor(Item item) : base(item)
        {

        }

        public override string GetExtraData()
        {
            return "";
        }
    }
}
