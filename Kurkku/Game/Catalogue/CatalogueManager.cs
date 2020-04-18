using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class CatalogueManager : ILoadable
    {
        #region Fields

        public static readonly CatalogueManager Instance = new CatalogueManager();

        #endregion

        #region Properties

        public List<CataloguePageData> Pages;
        public List<CatalogueItem> Items;
        public List<CataloguePackage> Packages;

        #endregion

        #region Constructors

        public void Load()
        {
            Pages = CatalogueDao.GetPages();
            Items = CatalogueDao.GetItems().Select(x => new CatalogueItem(x)).ToList();
            Packages = CatalogueDao.GetPackages().Select(x => new CataloguePackage(x)).ToList();
            DeserialisePageData();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Convert JSON arrays to list of images and strings
        /// </summary>
        public void DeserialisePageData()
        {
            foreach (var page in Pages)
            {
                page.Images = JsonConvert.DeserializeObject<List<string>>(page.ImagesData);
                page.Texts = JsonConvert.DeserializeObject<List<string>>(page.TextsData);
            }
        }

        /// <summary>
        /// Get applicable pages for parent id
        /// </summary>
        public List<CataloguePageData> GetPages(int parentId, int rank, bool hasClub)
        {
            var pages = Pages.Where(x => x.ParentId == parentId && x.IsEnabled && rank >= x.MinRank).ToList();

            if (!hasClub)
                pages = pages.Where(x => !x.IsClubOnly).ToList();

            return pages.OrderBy(x => x.OrderId).ToList();
        }

        /// <summary>
        /// Get page by page id
        /// </summary>
        public CataloguePageData GetPage(int pageId)
        {
            return Pages.Where(x => x.Id == pageId).FirstOrDefault();
        }

        /// <summary>
        /// Get applicable items for page id
        /// </summary>
        public List<CatalogueItem> GetItems(int pageId)
        {
            return Items.Where(x => x.PageIds.Contains(pageId)).OrderBy(x => x.Data.OrderId).ToList();
        }

        /// <summary>
        /// Get package by catalogue item sale code
        /// </summary>
        public CataloguePackage GetPackage(string saleCode)
        {
            return Packages.Where(x => x.Data.SaleCode == saleCode).FirstOrDefault();
        }

        #endregion
    }
}
