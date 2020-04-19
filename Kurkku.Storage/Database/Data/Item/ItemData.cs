using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    class ItemMapping : ClassMap<ItemData>
    {
        public ItemMapping()
        {
            Table("item");
            Id(x => x.Id, "id");
            Map(x => x.OrderId, "sprite");
            Map(x => x.UserId, "name");
            Map(x => x.RoomId, "sprite_id");
            Map(x => x.DefinitionId, "length");
            Map(x => x.X, "width");
            Map(x => x.Y, "top_height");
            Map(x => x.Z, "max_status");
            Map(x => x.WallPosition, "behaviour");
            Map(x => x.Rotation, "interactor");
            Map(x => x.ExtraData, "is_tradable");
        }
    }

    public class ItemData
    {
        public virtual int Id { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int UserId { get; set; }
        public virtual int RoomId { get; set; }
        public virtual int DefinitionId { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual double Z { get; set; }
        public virtual string WallPosition { get; set; }
        public virtual int Rotation { get; set; }
        public virtual bool ExtraData { get; set; }
    }
}
