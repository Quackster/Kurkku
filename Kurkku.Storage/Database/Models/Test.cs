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
    }
}
