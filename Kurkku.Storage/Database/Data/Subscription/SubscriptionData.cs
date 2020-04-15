using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    public class SubscriptionDataMapping : ClassMap<SubscriptionData>
    {
        public SubscriptionDataMapping()
        {
            Table("user_subscriptions");
            Id(x => x.UserId, "user_id").GeneratedBy.Assigned();
            Map(x => x.SubscribedDate, "subscribed_at");
            Map(x => x.ExpireDate, "expire_at");
        }
    }

    public class SubscriptionData
    {
        public virtual int UserId { get; set; }
        public virtual DateTime SubscribedDate { get; set; }
        public virtual DateTime ExpireDate { get; set; }

        public virtual int MonthsLeft
        {
            get
            {
                if (DateTime.Now > ExpireDate)
                    return 0;

                TimeSpan ts = ExpireDate.Subtract(DateTime.Now);
                return ts.TotalDays > 0 ? (int)ts.TotalDays / 30 : 0;
            } 
        }

        public virtual int DaysLeft
        {
            get
            {
                if (DateTime.Now > ExpireDate)
                    return 0;

                TimeSpan ts = ExpireDate.Subtract(DateTime.Now);
                var monthsLeft = MonthsLeft;

                // Remove the months left from the remaining time, so we just get the days left..
                if (monthsLeft > 0)
                {
                    var expireDate = ExpireDate.AddDays((monthsLeft * 30) * -1); // Turn the months left into days into a negative number by multiplying by -1
                    ts = expireDate.Subtract(DateTime.Now);
                }

                return ts.Days;
            }
        }
    }
}
