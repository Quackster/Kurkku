using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class RollerEntityTask : IRollerTask<IEntity>
    {
        #region Public methods

        public Position CanRoll(IEntity entity, Item roller, Room room)
        {
            return null;
        }

        public void DoRoll(IEntity entity, Item roller, Room room, Position fromPosition, Position nextPosition)
        {

        }

        #endregion
    }
}
