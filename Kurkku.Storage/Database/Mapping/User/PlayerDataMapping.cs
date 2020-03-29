using FluentNHibernate.Mapping;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Models
{
    public class PlayerDataMapping : ClassMap<PlayerData>
    {
        public PlayerDataMapping()
        {
            Table("user");
            Id(x => x.Id, "id");
            Map(x => x.Username, "username");
            Map(x => x.Figure, "figure");
            Map(x => x.Sex, "sex");
            Map(x => x.Rank, "rank");
            Map(x => x.Credits, "credits");
            Map(x => x.Pixels, "pixels");
            Map(x => x.JoinDate, "join_date");
            Map(x => x.LastOnline, "last_online");
        }
    }
}
