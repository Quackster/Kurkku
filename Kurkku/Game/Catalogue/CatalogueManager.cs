using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
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
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get applicable pages for parent id
        /// </summary>
        public List<CataloguePageData> GetPages(int parentId, int rank, bool hasClub)
        {
            var pages = Pages.Where(x => x.ParentId == parentId && x.IsEnabled).ToList();

            if (!hasClub)
                pages = pages.Where(x => !x.IsClubOnly).ToList();

            return pages;
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
