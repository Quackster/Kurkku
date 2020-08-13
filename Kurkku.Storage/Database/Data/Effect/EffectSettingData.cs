using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Data
{
    public class EffectSettingMapping : ClassMap<EffectSettingData>
    {
        public EffectSettingMapping()
        {
            Table("effects");
            Id(x => x.EffectId, "effect_id");
            Map(x => x.Duration, "duration");
            Map(x => x.IsCostume, "is_costume");
        }
    }

    public class EffectSettingData
    {
        public virtual int EffectId { get; set; }
        public virtual int Duration { get; set; }
        public virtual bool IsCostume { get; set; }
    }
}
