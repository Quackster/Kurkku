using Kurkku.Storage.Database.Access;
using Kurkku.Storage.Database.Data;
using System.Collections.Generic;
using System.Linq;
using Kurkku.Util.Extensions;

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

        #region Promotion

        /// <summary>
        /// Get a random popular promotion
        /// </summary>
        /// <returns></returns>
        public PublicItemData GetPopularPromotion()
        {
            var publicItemsList = NavigatorDao.GetPublicItems().Where(x => x.Room != null && x.Room.UsersNow > 0).ToList();

            if (publicItemsList.Count > 0)
                return publicItemsList.PickRandom();

            return null;
        }

        #endregion
    }
}
