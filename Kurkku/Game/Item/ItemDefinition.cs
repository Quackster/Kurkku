using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public class ItemDefinition
    {
        #region Properties

        public ItemDefinitionData Data { get; }
        public List<ItemBehaviour> Behaviours { get; }
        public object Type
        {
            get
            {
                if (Behaviours.Contains(ItemBehaviour.WALL_ITEM))
                    return "i";

                if (Behaviours.Contains(ItemBehaviour.EFFECT))
                    return "e";

                return "s";
            }
        }

        #endregion

        #region Constructors

        public ItemDefinition(ItemDefinitionData data)
        {
            Data = data;
            Behaviours = new List<ItemBehaviour>();
            ParseBehaviours();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Parse behaviour string
        /// </summary>
        private void ParseBehaviours()
        {
            foreach (string behaviourData in Data.Behaviour.Split(','))
            {
                try
                {
                    ItemBehaviour behaviour = (ItemBehaviour) Enum.Parse(typeof(ItemBehaviour), behaviourData.ToUpper());
                    Behaviours.Add(behaviour);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Get if definition has behaviour
        /// </summary>
        public bool HasBehaviour(ItemBehaviour behaviour)
        {
            return Behaviours.Contains(behaviour);
        }

        #endregion
    }
}
