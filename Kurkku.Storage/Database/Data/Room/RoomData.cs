
using FluentNHibernate.Mapping;
using System;

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
            Map(x => x.CategoryId, "category_id");
            Map(x => x.UsersNow, "visitors_now");
            Map(x => x.UsersMax, "visitors_max");
            Map(x => x.Status, "status");
            Map(x => x.Password, "password");
            Map(x => x.Model, "model");
            Map(x => x.CCTs, "ccts");
            Map(x => x.Wallpaper, "wallpaper");
            Map(x => x.Floor, "floor");
            Map(x => x.Landscape, "landscape");
            Map(x => x.AllowPets, "allow_pets");
            Map(x => x.AllowPetsEat, "allow_pets_eat");
            Map(x => x.AllowWalkthrough, "allow_walkthrough");
            Map(x => x.IsHidingWall, "hidewall");
            Map(x => x.WallThickness, "wall_thickness");
            Map(x => x.FloorThickness, "floor_thickness");
            Map(x => x.Rating, "rating");

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
        public virtual string Wallpaper { get; set; }
        public virtual string Floor { get; set; }
        public virtual string Landscape { get; set; }
        public virtual bool AllowPets { get; set; }
        public virtual bool AllowPetsEat { get; set; }
        public virtual bool AllowWalkthrough { get; set; }
        public virtual bool IsHidingWall { get; set; }
        public virtual int WallThickness { get; set; }
        public virtual int FloorThickness { get; set; }
        public virtual PlayerData OwnerData { get; set; }
        public virtual NavigatorCategoryData Category { get; set; }
        public virtual int Rating { get; set; }
        public virtual bool IsPublicRoom => OwnerData == null;
        public virtual int UsersNow { get; set; }
        public virtual int UsersMax { get; set; }
    }

    public enum RoomStatus
    {
        OPEN = 0,
        CLOSED = 1,
        PASSWORD = 2
    }
}
