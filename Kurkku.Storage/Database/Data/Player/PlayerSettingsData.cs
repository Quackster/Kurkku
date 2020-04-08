using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    public class PlayerSettingsMapping : ClassMap<PlayerSettingsData>
    {
        public PlayerSettingsMapping()
        {
            Table("user_settings");
            Id(x => x.UserId, "user_id").GeneratedBy.Assigned();
            Map(x => x.Respect, "respect_points").Generated.Insert();
            Map(x => x.DailyRespectPoints, "daily_respect_points").Generated.Insert();
            Map(x => x.DailyPetRespectPoints, "daily_respect_pet_points").Generated.Insert();
            Map(x => x.FriendRequestsEnabled, "friend_requests_enabled").Generated.Insert();
            Map(x => x.FollowingEnabled, "following_enabled").Generated.Insert();
        }
    }
    
    public class PlayerSettingsData 
    {
        public virtual int UserId { get; set; }
        public virtual int Respect { get; set; }
        public virtual int DailyRespectPoints { get; set; }
        public virtual int DailyPetRespectPoints { get; set; }
        public virtual bool FriendRequestsEnabled { get; set; }
        public virtual bool FollowingEnabled { get; set; }
    }
}
