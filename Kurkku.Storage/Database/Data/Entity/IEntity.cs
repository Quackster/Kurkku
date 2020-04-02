namespace Kurkku.Storage.Database.Data
{
    public interface IEntity<T> where T : IEntityData
    {
        T Data { get; }
    }
}
