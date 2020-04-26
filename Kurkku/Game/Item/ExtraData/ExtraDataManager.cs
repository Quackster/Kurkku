using Kurkku.Messages;
using System;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public class ExtraDataManager
    {
        #region Fields

        public static readonly ExtraDataManager Instance = new ExtraDataManager();

        #endregion


        #region Public methods

        /// <summary>
        /// Write the relevant extra data to the packet
        /// </summary>
        public void WriteExtraData(IMessageComposer composer, Interactor interactor)
        {
            switch (interactor.ExtraDataType)
            {
                case ExtraDataType.StringData:
                    composer.Data.Add(0);
                    composer.Data.Add((string)interactor.GetExtraData());
                    break;
            }
        }

        #endregion
    }
}
