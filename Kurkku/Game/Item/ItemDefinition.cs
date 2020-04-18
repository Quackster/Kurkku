using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    public class ItemDefinition
    {
        #region Properties

        public ItemDefinitionData Data { get; }

        #endregion

        #region Constructors

        public ItemDefinition(ItemDefinitionData data)
        {
            Data = data;
        }

        #endregion
    }
}
