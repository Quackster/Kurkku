using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class NavigatorManager : ILoadable
    {
        #region Fields

        public static readonly NavigatorManager Instance = new NavigatorManager();

        #endregion

        #region Properties

        public List<NavigatorCategoryData> Categories;

        #endregion

        #region Constructors

        public void Load()
        {
            Categories = NavigatorDao.GetCategories();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get applicable categories for rank
        /// </summary>
        public List<NavigatorCategoryData> GetCategories(int rank)
        {
            return Categories.Where(x => (rank >= x.MinimumRank)).ToList();
        }

        #endregion
    }
}
