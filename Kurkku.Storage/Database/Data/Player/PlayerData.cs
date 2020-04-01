using FluentNHibernate.Mapping;
using Kurkku.Storage.Database.Data.Entity;
using System;

namespace Kurkku.Storage.Database.Data.Player
{
    public class PlayerDataMapping : ClassMap<PlayerData>
    {
        public PlayerDataMapping()
        {
            Table("user");
            Id(x => x.Id, "id");
            Map(x => x.Name, "username");
            Map(x => x.Figure, "figure");
            Map(x => x.Sex, "sex");
            Map(x => x.Rank, "rank");
            Map(x => x.Credits, "credits");
            Map(x => x.Pixels, "pixels");
            Map(x => x.JoinDate, "join_date");
            Map(x => x.LastOnline, "last_online");
        }
    }

    public class PlayerData : IEntityData
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Figure { get; set; }
        public virtual string Sex { get; set; }
        public virtual int Rank { get; set; }
        public virtual int Credits { get; set; }
        public virtual int Pixels { get; set; }
        public virtual DateTime JoinDate { get; set; }
        public virtual DateTime LastOnline { get; set; }
    }
}
