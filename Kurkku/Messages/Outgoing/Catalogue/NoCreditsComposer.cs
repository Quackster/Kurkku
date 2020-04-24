using Kurkku.Storage.Database.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Messages.Outgoing
{
    public class NoCreditsComposer : IMessageComposer
    {
        private bool hasEnoughCredits;
        private bool hasEnoughSeasonalCurrency;
        private SeasonalCurrencyType? seasonalCurrencyType;

        public NoCreditsComposer(bool hasEnoughCredits, bool hasEnoughSeasonalCurrency)
        {
            this.hasEnoughCredits = hasEnoughCredits;
            this.hasEnoughSeasonalCurrency = hasEnoughSeasonalCurrency;
        }

        public NoCreditsComposer(bool hasEnoughCredits, bool hasEnoughSeasonalCurrency, SeasonalCurrencyType seasonalCurrencyType)
        {
            this.hasEnoughCredits = hasEnoughCredits;
            this.hasEnoughSeasonalCurrency = hasEnoughSeasonalCurrency;
            this.seasonalCurrencyType = seasonalCurrencyType;
        }

        public override void Write()
        {
            m_Data.Add(hasEnoughCredits);
            m_Data.Add(hasEnoughSeasonalCurrency);

            if (seasonalCurrencyType != null)
                m_Data.Add((int)seasonalCurrencyType.Value);
        }
    }
}
