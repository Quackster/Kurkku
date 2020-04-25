using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    class ItemMapping : ClassMap<ItemData>
    {
        public ItemMapping()
        {
            Table("item");
            Id(x => x.Id, "id");
            Map(x => x.OrderId, "order_id");
            Map(x => x.OwnerId, "user_id");
            Map(x => x.RoomId, "room_id");
            Map(x => x.DefinitionId, "definition_id");
            Map(x => x.X, "x").Generated.Insert();
            Map(x => x.Y, "y").Generated.Insert();
            Map(x => x.Z, "z").Generated.Insert();
            Map(x => x.WallPosition, "wall_position");
            Map(x => x.Rotation, "rotation").Generated.Insert();
            Map(x => x.ExtraData, "custom_data");

            References(x => x.OwnerData, "user_id")
                .ReadOnly()
                .NotFound.Ignore();
        }
    }

    public class ItemData
    {
        public ItemData()
        {
            Id = Guid.NewGuid().ToString();
            WallPosition = "";
        }

        public virtual string Id { get; set; }
        public virtual int OrderId { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual PlayerData OwnerData { get; set; }
        public virtual int RoomId { get; set; }
        public virtual int DefinitionId { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public virtual double Z { get; set; }
        public virtual string WallPosition { get; set; }
        public virtual int Rotation { get; set; }
        public virtual string ExtraData { get; set; }
    }
}
