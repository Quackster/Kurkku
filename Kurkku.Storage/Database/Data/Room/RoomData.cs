
using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    public class RoomMapping : ClassMap<RoomData>
    {
        public RoomMapping()
        {
            Table("room");
            Id(x => x.Id, "id");
            Map(x => x.OwnerId, "owner_id");
            Map(x => x.Name, "name");
            Map(x => x.Description, "description");
            Map(x => x.CategoryId, "category_id").Generated.Insert();
            Map(x => x.UsersNow, "visitors_now").Generated.Insert();
            Map(x => x.UsersMax, "visitors_max").Generated.Insert();
            Map(x => x.Status, "status").Generated.Insert();
            Map(x => x.Password, "password").Generated.Insert();
            Map(x => x.Model, "model");
            Map(x => x.CCTs, "ccts").Generated.Insert();
            Map(x => x.Wallpaper, "wallpaper").Generated.Insert();
            Map(x => x.Floor, "floor").Generated.Insert();
            Map(x => x.Landscape, "landscape").Generated.Insert();
            Map(x => x.AllowPets, "allow_pets").Generated.Insert();
            Map(x => x.AllowPetsEat, "allow_pets_eat").Generated.Insert();
            Map(x => x.AllowWalkthrough, "allow_walkthrough").Generated.Insert();
            Map(x => x.IsHidingWall, "hidewall").Generated.Insert();
            Map(x => x.WallThickness, "wall_thickness").Generated.Insert();
            Map(x => x.FloorThickness, "floor_thickness").Generated.Insert();
            Map(x => x.Rating, "rating").Generated.Insert();
            Map(x => x.IsOwnerHidden, "is_owner_hidden");

            References(x => x.OwnerData, "owner_id")
                .ReadOnly()
                .Cascade.None()
                .NotFound.Ignore();

            References(x => x.Category, "category_id")
                .ReadOnly()
                .Cascade.None()
                .NotFound.Ignore();
        }
    }

    public class RoomData
    {
        public virtual int Id { get; set; }
        public virtual int OwnerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual RoomStatus Status { get; set; }
        public virtual string Password { get; set; }
        public virtual string Model { get; set; }
        public virtual string CCTs { get; set; }
        public virtual int Wallpaper { get; set; }
        public virtual int Floor { get; set; }
        public virtual int Landscape { get; set; }
        public virtual bool AllowPets { get; set; }
        public virtual bool AllowPetsEat { get; set; }
        public virtual bool AllowWalkthrough { get; set; }
        public virtual bool IsHidingWall { get; set; }
        public virtual int WallThickness { get; set; }
        public virtual int FloorThickness { get; set; }
        public virtual PlayerData OwnerData { get; set; }
        public virtual NavigatorCategoryData Category { get; set; }
        public virtual int Rating { get; set; }
        public virtual bool IsPrivateRoom => OwnerId > 0;
        public virtual bool IsPublicRoom => OwnerId == 0;
        public virtual int UsersNow { get; set; }
        public virtual int UsersMax { get; set; }
        public virtual bool IsMuted { get; set; }
        public virtual bool IsOwnerHidden { get; set; }
    }

    public enum RoomStatus
    {
        OPEN = 0,
        CLOSED = 1,
        PASSWORD = 2
    }
}
