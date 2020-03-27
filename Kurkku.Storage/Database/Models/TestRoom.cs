
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Models
{
    class TestRoomMap : ClassMap<TestRoom>
    {
        public TestRoomMap()
        {
            Table("test_room");
            Id(x => x.TestId, "test_id");
            Map(x => x.Name, "name");
        }
    }

    public class TestRoom
    {
        public virtual string TestId
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
