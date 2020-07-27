
using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    public class TagMapping : ClassMap<TagData>
    {
        public TagMapping()
        {
            Table("tags");
            Map(x => x.UserId, "user_id").Generated.Insert();
            Map(x => x.RoomId, "room_id").Generated.Insert();
            Map(x => x.Text, "text").Generated.Insert();

            References(x => x.Room, "room_id")
                .ReadOnly()
                .Cascade.None()
                .NotFound.Ignore();

            References(x => x.User, "user_id")
                .ReadOnly()
                .Cascade.None()
                .NotFound.Ignore();

            CompositeId()
                .KeyProperty(x => x.UserId, "user_id")
                .KeyProperty(x => x.RoomId, "room_id")
                .KeyProperty(x => x.Text, "text");
        }
    }

    public class TagData
    {
        public virtual int UserId { get; set; }
        public virtual int RoomId { get; set; }
        public virtual PlayerData User { get; set; }
        public virtual RoomData Room { get; set; }
        public virtual string Text { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as TagData;

            if (t == null)
                return false;

            if (UserId == t.UserId && 
                RoomId == t.RoomId &&
                Text == t.Text)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
