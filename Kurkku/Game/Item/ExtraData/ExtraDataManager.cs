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
        public void WriteExtraData(IMessageComposer composer, Item item, bool inventoryView = false)
        {
            var interactor = item.Interactor;
            composer.Data.Add((int)interactor.ExtraDataType);

            switch (interactor.ExtraDataType)
            {
                case ExtraDataType.StringData:
                    composer.Data.Add((string)interactor.GetExtraData(inventoryView));
                    break;
            }
        }

        #endregion
    }
}
