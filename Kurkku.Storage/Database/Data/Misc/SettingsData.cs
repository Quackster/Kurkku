using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    public class SettingsDataMapping : ClassMap<SettingsData>
    {
        public SettingsDataMapping()
        {
            Table("server_settings");
            Id(x => x.Key, "setting");
            Map(x => x.Value, "value");
        }
    }

    public class SettingsData
    {
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
    }
}
