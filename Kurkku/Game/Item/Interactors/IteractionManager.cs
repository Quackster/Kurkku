using System;
using System.Collections.Generic;

namespace Kurkku.Game
{
    public class InteractionManager : ILoadable
    {
        #region Fields

        public static readonly InteractionManager Instance = new InteractionManager();

        #endregion

        #region Properties

        public Dictionary<InteractorType, Type> Interactors { get; set; }

        #endregion

        #region Constructors

        public void Load()
        {
            Interactors = new Dictionary<InteractorType, Type>();
            Interactors[InteractorType.DEFAULT] = typeof(DefaultInteractor);
            Interactors[InteractorType.POST_IT] = typeof(StickieInteractor);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Create interactor instance for item
        /// </summary>
        public Interactor CreateInteractor(Item item)
        {
            Type type;

            if (!Interactors.TryGetValue(item.Definition.InteractorType, out type))
                type = Interactors[InteractorType.DEFAULT];

            return (Interactor)Activator.CreateInstance(type, item);
        }

        #endregion
    }
}
