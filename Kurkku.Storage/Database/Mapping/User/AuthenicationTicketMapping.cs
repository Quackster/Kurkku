using FluentNHibernate.Mapping;
using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Models
{
    public class AuthenicationTicketMapping : ClassMap<AuthenicationTicketData>
    {
        public AuthenicationTicketMapping()
        {
            Table("authentication_ticket");
            Id(x => x.UserId, "user_id").GeneratedBy.Assigned();
            Map(x => x.Ticket, "sso_ticket");
            Map(x => x.ExpireDate, "expires_at").Nullable();
            References(x => x.PlayerData, "user_id").NotFound.Ignore();

            /*Join("user", m =>
            {
                m.Fetch.Select().Inverse();
                m.KeyColumn("id");
                m.References(x => x.PlayerData, "id");
            });*/
            /*
            Join("user_test", m =>
            {
                m.Fetch.Select().Inverse();
                m.KeyColumn("random");
                m.References(x => x.TestData, "random");
            });*/
        }
    }
}
