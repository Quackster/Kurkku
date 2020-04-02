namespace Kurkku.Storage.Database.Data
{
    interface IEntity<T> where T : IEntityData
    {
        T Data { get; }
    }
}
