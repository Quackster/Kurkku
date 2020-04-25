using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public abstract class Interactor
    {
        protected Item Item { get; }

        protected Interactor(Item item)
        {
            Item = item;
        }
    }
}
