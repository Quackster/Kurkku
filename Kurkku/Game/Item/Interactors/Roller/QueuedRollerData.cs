using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class QueuedRollerData
    {
        #region Properties

        public Item Roller { get; set; }
        public List<RollingData> RollingItems { get; set; }
        public IEntity RollingEntity { get; set; }

        #endregion

        #region Constructor

        public QueuedRollerData(Item roller)
        {
            Roller = roller;
            RollingItems = new List<RollingData>();
        }

        #endregion
    }
}
