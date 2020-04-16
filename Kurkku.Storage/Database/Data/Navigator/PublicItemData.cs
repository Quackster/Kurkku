using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    public class PublicItemMapping : ClassMap<PublicItemData>
    {
        public PublicItemMapping()
        {
            Table("navigator_official_rooms");
            Id(x => x.BannerId, "banner_id");
            Map(x => x.ParentId, "parent_id");
            Map(x => x.BannerType, "banner_type");
            Map(x => x.RoomId, "room_id");
            Map(x => x.ImageType, "image_type");
            Map(x => x.Label, "label");
            Map(x => x.Description, "description");
            Map(x => x.DescriptionEntry, "description_entry");
            Map(x => x.Image, "image_url");

            References(x => x.Room, "room_id").NotFound.Ignore();
        }
    }

    public class PublicItemData
    {
        public virtual int BannerId { get; set; }
        public virtual int ParentId { get; set; }
        public virtual BannerType BannerType { get; set; }
        public virtual int RoomId { get; set; }
        public virtual ImageType ImageType { get; set; }
        public virtual string Label { get; set; }
        public virtual string Description { get; set; }
        public virtual int DescriptionEntry { get; set; }
        public virtual string Image { get; set; }

        public virtual RoomData Room { get; set; }
    }

    public enum BannerType
    {
        // 'NONE','TAG','FLAT','PUBLIC_FLAT','CATEGORY'
        NONE = 0,
        TAG = 1,
        FLAT = 2,
        PUBLIC_FLAT = 3,
        CATEGORY = 4
    }

    public enum ImageType
    {
        INTERNAL = 0,
        EXTERNAL = 1
    }
}
