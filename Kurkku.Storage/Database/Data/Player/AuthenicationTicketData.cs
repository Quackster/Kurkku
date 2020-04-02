using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    public class AuthenicationTicketMapping : ClassMap<AuthenicationTicketData>
    {
        public AuthenicationTicketMapping()
        {
            Table("authentication_ticket");

            CompositeId()
                .KeyProperty(x => x.UserId, "user_id")
                .KeyProperty(x => x.Ticket, "sso_ticket");

            Map(x => x.UserId, "user_id");
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

    public class AuthenicationTicketData
    {
        public virtual int UserId { get; set; }
        public virtual string Ticket { get; set; }
        public virtual DateTime? ExpireDate { get; set; }
        public virtual PlayerData PlayerData { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as AuthenicationTicketData;

            if (t == null)
                return false;

            if (Ticket == t.Ticket && UserId == t.UserId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
