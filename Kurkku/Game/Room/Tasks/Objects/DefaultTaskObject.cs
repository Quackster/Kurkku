using System;
using System.Collections.Generic;
using System.Text;
using static Kurkku.Game.DiceInteractor;

namespace Kurkku.Game
{
    public class DefaultTaskObject : ITaskObject
    {
        #region Constructor

        public DefaultTaskObject(Item item) : base(item) { }

        #endregion

        #region Public methods

        public override void OnTick() { }
        public override void OnTickComplete() { }

        #endregion
    }
}
