using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace Kurkku.Storage.Database.Data
{
    class CataloguePageMapping : ClassMap<CataloguePageData>
    {
        public CataloguePageMapping()
        {
            Table("catalogue_pages");
            Id(x => x.Id, "id");
            Map(x => x.ParentId, "parent_id");
            Map(x => x.OrderId, "order_id");
            Map(x => x.Caption, "caption");
            Map(x => x.MinRank, "min_rank");
            Map(x => x.IconColour, "icon_colour");
            Map(x => x.IconImage, "icon_image");
            Map(x => x.IsNavigatable, "is_navigatable");
            Map(x => x.IsEnabled, "is_enabled");
            Map(x => x.IsClubOnly, "is_club_only");
            Map(x => x.Layout, "layout");
            Map(x => x.ImagesData, "images");
            Map(x => x.TextsData, "texts");
        }
    }

    public class CataloguePageData
    {
        public virtual int Id { get; set; }
        public virtual int ParentId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual string Caption { get; set; }
        public virtual int MinRank { get; set; }
        public virtual int IconColour { get; set; }
        public virtual int IconImage { get; set; }
        public virtual bool IsVisible { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual bool IsNavigatable { get; set; }
        public virtual bool IsClubOnly { get; set; }
        public virtual string Layout { get; set; }
        public virtual string ImagesData { get; set; }
        public virtual string TextsData { get; set; }
        public virtual List<string> Images { get; set; }
        public virtual List<string> Texts { get; set; }
    }
}
