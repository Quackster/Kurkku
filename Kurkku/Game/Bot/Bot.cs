using Kurkku.Storage.Database.Data;
using System;

namespace Kurkku.Game
{
    public class Bot : IEntity
    {
        public IEntityData EntityData => throw new NotImplementedException();

        public RoomEntity RoomEntity => throw new NotImplementedException();
    }
}
