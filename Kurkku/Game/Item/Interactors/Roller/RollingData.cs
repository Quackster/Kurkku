using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class RollingData
    {
        #region Properties

        public Item Roller { get; set; }
        public IEntity RollingEntity { get; set; }
        public Item RollingItem { get; set; }
        public Position FromPosition { get; set; }
        public Position NextPosition { get; set; }
        public double DisplayHeight { get; set; }
        public double HeightUpdate { get; set; }

        #endregion
    }
}
