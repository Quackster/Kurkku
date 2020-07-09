using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public interface IRollerTask<T>
    {
        #region Methods

        void TryGetRollingData(T rollingType, Item roller, Room room, out Position nextPosition);
        void DoRoll(T rollingType, Item roller, Room room, Position fromPosition, Position nextPosition);

        #endregion
    }
}
