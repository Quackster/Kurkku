using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    public interface IEntity
    {
        IEntityData Data { get; }

        RoomEntity RoomEntity { get; }
    }
}
