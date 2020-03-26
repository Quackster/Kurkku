using FluentNHibernate.Mapping;
using Kurkku.Storage.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Mapping
{
    class TestMap : ClassMap<Test>
    {
        public TestMap()
        {
            Id(x => x.Id, "id");
            Map(x => x.TestId, "test_id");
            Map(x => x.User, "user");
        }
    }
}
