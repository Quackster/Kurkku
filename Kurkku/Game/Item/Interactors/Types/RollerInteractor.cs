using Kurkku.Messages.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kurkku.Game
{
    public class RollerInteractor : Interactor
    {
        #region Fields

        public RollerTaskObject taskObject;

        #endregion

        #region Overridden Properties

        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;
        public override ITaskObject TaskObject => taskObject;

        #endregion

        #region Constructor

        public RollerInteractor(Item item) : base(item)
        {
            this.taskObject = new RollerTaskObject(item);
        }

        #endregion
    }
}
