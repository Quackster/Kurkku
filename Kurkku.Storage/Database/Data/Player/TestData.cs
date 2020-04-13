using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    public class TestDataMapping : ClassMap<TestData>
    {
        public TestDataMapping()
        {
            Table("test");
            Id(x => x.Id, "id");
            Map(x => x.Test, "test");
        }
    }

    public class TestData
    {
        public virtual int Id { get; set; }
        public virtual string Test { get; set; }
    }
}
