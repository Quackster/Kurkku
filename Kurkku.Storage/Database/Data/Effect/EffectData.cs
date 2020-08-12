using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    class EffectMapping: ClassMap<EffectData>
    {
        public EffectMapping()
        {
            Table("user_effects");
            Map(x => x.ExpireAt, "expire_at").Generated.Insert().Nullable();
            Map(x => x.Quantity, "quantity").Generated.Insert();
            Map(x => x.IsActivated, "is_activated").Generated.Insert();

            CompositeId()
                .KeyProperty(x => x.UserId, "user_id")
                .KeyProperty(x => x.EffectId, "effect_id");
        }
    }

    public class EffectData
    {
        public virtual int UserId { get; set; }
        public virtual int EffectId { get; set; }
        public virtual DateTime? ExpireAt { get; set; }
        public virtual int Quantity { get; set; }
        public virtual bool IsActivated { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as EffectData;

            if (t == null)
                return false;

            if (UserId == t.UserId && EffectId == t.EffectId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
