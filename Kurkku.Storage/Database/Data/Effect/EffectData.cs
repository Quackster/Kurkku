using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    class EffectMapping: ClassMap<EffectData>
    {
        public EffectMapping()
        {
            Table("user_effects");
            Id(x => x.Id, "id").GeneratedBy.Guid();
            Map(x => x.UserId, "user_id");
            Map(x => x.EffectId, "effect_id");
            Map(x => x.ExpireAt, "expire_at").Generated.Insert().Nullable();
            Map(x => x.IsActivated, "is_activated").Generated.Insert();
        }
    }

    public class EffectData
    {
        public virtual Guid Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int EffectId { get; set; }
        public virtual DateTime? ExpireAt { get; set; }
        public virtual bool IsActivated { get; set; }
    }
}
