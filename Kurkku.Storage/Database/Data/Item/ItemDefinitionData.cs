using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    class ItemDefinitionMapping : ClassMap<ItemDefinitionData>
    {
        public ItemDefinitionMapping()
        {
            Table("item_definitions");
            Id(x => x.Id, "id");
            Map(x => x.Sprite, "sprite");
            Map(x => x.Name, "name");
            Map(x => x.SpriteId, "sprite_id");
            Map(x => x.Length, "length");
            Map(x => x.Width, "width");
            Map(x => x.TopHeight, "top_height");
            Map(x => x.MaxStatus, "max_status");
            Map(x => x.Behaviour, "behaviour");
            Map(x => x.Interactor, "interactor");
            Map(x => x.IsTradable, "is_tradable");
            Map(x => x.IsRecyclable, "is_recyclable");
            Map(x => x.IsStackable, "is_stackable");
            Map(x => x.IsSellable, "is_sellable");
            Map(x => x.DrinkIds, "drink_ids");
        }
    }

    public class ItemDefinitionData
    {
        public virtual int Id { get; set; }
        public virtual string Sprite { get; set; }
        public virtual string Name { get; set; }
        public virtual int SpriteId { get; set; }
        public virtual int Length { get; set; }
        public virtual int Width { get; set; }
        public virtual double TopHeight { get; set; }
        public virtual int MaxStatus { get; set; }
        public virtual string Behaviour { get; set; }
        public virtual string Interactor { get; set; }
        public virtual bool IsTradable { get; set; }
        public virtual bool IsRecyclable { get; set; }
        public virtual bool IsStackable { get; set; }
        public virtual bool IsSellable { get; set; }
        public virtual string DrinkIds { get; set; }
    }
}
