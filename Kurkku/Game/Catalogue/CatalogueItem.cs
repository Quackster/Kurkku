using Kurkku.Util.Extensions;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    public class CatalogueItem
    {
        #region Properties

        public CatalogueItemData Data { get; }
        public ItemDefinition Definition => ItemManager.Instance.GetDefinition(Data.DefinitionId);
        public CataloguePackage Package => CatalogueManager.Instance.GetPackage(Data.SaleCode);
        public int[] PageIds { get; }

        #endregion

        #region Constructors

        public CatalogueItem(CatalogueItemData data)
        {
            Data = data;
            PageIds = Data.PageId.ToIntArray(',');
        }

        #endregion

        #region Public methods


        #endregion
    }
}
