using Kurkku.Storage.Database.Data;

namespace Kurkku.Game
{
    public interface IEntity<T> where T : IEntityData
    {
        T Data { get; }
    }
}
