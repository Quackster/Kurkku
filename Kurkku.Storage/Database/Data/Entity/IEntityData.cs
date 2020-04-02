namespace Kurkku.Storage.Database.Data
{
    public interface IEntityData
    {
        int Id { get; set; }
        string Name { get; set; }
        string Figure { get; set; }
        string Sex { get; set; }
    }
}
