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

        #endregion

        #region Constructors

        public void Load()
        {
            Pages = CatalogueDao.GetPages();
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
            var pages = Pages.Where(x => x.ParentId == parentId && x.IsEnabled).ToList();

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

        #endregion
    }
}
