using FluentNHibernate.Mapping;
using System;

namespace Kurkku.Storage.Database.Data
{
    /*
    public class PlayerStatisticsMapping : ClassMap<PlayerStatisticsData>
    {
        public PlayerStatisticsMapping()
        {

        }
    }
    */
    public class PlayerStatisticsData 
    {
        public virtual int Respect { get; set; }
        public virtual int DailyRespectPoints { get; set; }
        public virtual int DailyPetRespectPoints { get; set; }

        public PlayerStatisticsData()
        {
            Respect = 0;
            DailyRespectPoints = 3;
            DailyPetRespectPoints = 3;
        }
    }
}
