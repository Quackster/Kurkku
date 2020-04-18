using Kurkku.Storage.Database.Access;
using System.Collections.Generic;
using System.Linq;

namespace Kurkku.Game
{
    public class ItemManager : ILoadable
    {
        #region Fields

        public static readonly ItemManager Instance = new ItemManager();

        #endregion

        #region Properties

        public Dictionary<int, ItemDefinition> Definitions { get; set; }

        #endregion

        #region Constructors

        public void Load()
        {
            Definitions = ItemDao.GetDefinitions().Select(x => new ItemDefinition(x)).ToDictionary(x => x.Data.Id, x => x);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Safe method to try and get definition
        /// </summary>
        public ItemDefinition GetDefinition(int definitionId)
        {
            Definitions.TryGetValue(definitionId, out var definition);
            return definition;
        }

        #endregion
    }
}
