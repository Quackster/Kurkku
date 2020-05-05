﻿using Kurkku.Util.Extensions;
using Newtonsoft.Json;
using System.Text;

namespace Kurkku.Game
{
    public class ChairInteractor : Interactor
    {
        public override ExtraDataType ExtraDataType => ExtraDataType.StringData;

        public ChairInteractor(Item item) : base(item) { }

        public override void OnStop(IEntity entity)
        {
            entity.RoomEntity.Position.Rotation = Item.Position.Rotation;
            entity.RoomEntity.AddStatus("sit", Item.Definition.Data.TopHeight.ToClientValue());
            entity.RoomEntity.NeedsUpdate = true;
        }
    }
}
