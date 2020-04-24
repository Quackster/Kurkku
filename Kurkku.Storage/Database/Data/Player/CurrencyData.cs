using FluentNHibernate.Mapping;

namespace Kurkku.Storage.Database.Data
{
    class CurrencyMapping : ClassMap<CurrencyData>
    {
        public CurrencyMapping()
        {
            Table("user_seasonal_currencies");

            //Map(x => x.UserId, "user_id");
            //Map(x => x.SeasonalType, "seasonal_type");
            Map(x => x.Balance, "balance");

            CompositeId()
                .KeyProperty(x => x.UserId, "user_id")
                .KeyProperty(x => x.SeasonalType, "seasonal_type");
        }
    }

    public class CurrencyData
    {
        public virtual int UserId { get; set; }
        public virtual SeasonalCurrencyType SeasonalType { get; set; }
        public virtual int Balance { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var t = obj as CurrencyData;

            if (t == null)
                return false;

            if (UserId == t.UserId && SeasonalType == t.SeasonalType)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
