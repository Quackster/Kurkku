using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    public interface IEntity
    {
        IEntityData EntityData { get; }

        RoomEntity RoomEntity { get; }
    }
}
