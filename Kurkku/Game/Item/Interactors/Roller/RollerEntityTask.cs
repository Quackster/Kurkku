using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class RollerEntityTask : IRollerTask<IEntity>
    {
        #region Public methods

        public void TryGetRollingData(IEntity entity, Item roller, Room room, out Position nextPosition)
        {
            nextPosition = null;
        }

        public void DoRoll(IEntity entity, Item roller, Room room, Position fromPosition, Position nextPosition)
        {

        }

        #endregion
    }
}
