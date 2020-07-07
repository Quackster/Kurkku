using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public interface IRollerTask<T>
    {
        #region Methods

        Position CanRoll(T rollingType, Item roller, Room room);
        void DoRoll(T rollingType, Item roller, Room room, Position fromPosition, Position nextPosition);

        #endregion
    }
}
