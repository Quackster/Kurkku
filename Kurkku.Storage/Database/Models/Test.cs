using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Models
{
    class TestMap : ClassMap<Test>
    {
        public TestMap()
        {
            Table("test");
            Id(x => x.TestId, "test_id");
            Map(x => x.User, "user");

            Join("test_room", m =>
            {
                m.Fetch.Select().Inverse();
                //m.KeyColumn("test_id");
                m.References(x => x.Room, "test_id");
                //m.Map(t => t.Room).
                //m.Map(t => t.Name);

            });

            /*
            Join("FormFields", m =>
            {
                m.Fetch.Join();
                m.KeyColumn("FieldId");
                m.Map(t => t.DisplayOrder).Nullable();
            });
            */
        }
    }

    public class Test
    {
        public virtual string TestId
        {
            get;
            set;
        }

        public virtual string User
        {
            get;
            set;
        }

        public virtual TestRoom Room
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }
    }
}
