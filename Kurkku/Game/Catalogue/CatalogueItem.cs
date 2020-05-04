using Kurkku.Util.Extensions;
using Kurkku.Storage.Database.Data;
using System.Collections.Generic;
using System;

namespace Kurkku.Game
{
    [Serializable]
    public class CatalogueItem
    {
        #region Properties

        public CatalogueItemData Data { get; }
        public ItemDefinition Definition => ItemManager.Instance.GetDefinition(Data.DefinitionId);
        public int[] PageIds { get; }
        public bool AllowBulkPurchase { get; set; }
        public List<CataloguePackage> Packages
        {
            get
            {
                if (Data.IsPackage)
                    return CatalogueManager.Instance.GetPackages(Data.SaleCode);

                return List.Create(new CataloguePackage(new CataloguePackageData
                {
                    Id = Data.Id,
                    SaleCode = Data.SaleCode,
                    DefinitionId = Data.DefinitionId,
                    SpecialSpriteId = Data.SpecialSpriteId,
                    Amount = Data.Amount
                }, this));
            }
        }

        #endregion

        #region Constructors

        public CatalogueItem(CatalogueItemData data)
        {
            Data = data;
            PageIds = Data.PageId.ToIntArray(',');
            AllowBulkPurchase = data.AllowBulkPurchase;
        }

        #endregion

        #region Public methods


        #endregion
    }
}
