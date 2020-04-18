using System;
using System.Collections.Generic;
using System.Text;
using Kurkku.Util.Extensions;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    public class CataloguePackage
    {
        #region Properties

        public CataloguePackageData Data { get; }
        public ItemDefinition Definition => ItemManager.Instance.GetDefinition(Data.DefinitionId);
        public int[] PageIds { get; }

        #endregion

        #region Constructors

        public CataloguePackage(CataloguePackageData data)
        {
            Data = data;
        }

        #endregion

        #region Public methods


        #endregion
    }
}
