using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Game
{
    public class Item
    {
        #region Properties

        public int Id;
        public ItemData Data { get; }
        public ItemDefinition Definition => ItemManager.Instance.GetDefinition(Data.DefinitionId);
        public Interactor Interactor { get; }

        #endregion

        #region Constructors

        public Item(ItemData data)
        {
            Data = data;
            Interactor = InteractionManager.Instance.CreateInteractor(this);
            Id = ItemManager.Instance.GenerateId();
        }

        public string ExtraData => Interactor.GetExtraData();
       

        #endregion
    }
}
