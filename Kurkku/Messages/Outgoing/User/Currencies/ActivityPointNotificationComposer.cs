using Kurkku.Storage.Database.Data;
using System.Collections.Generic;

namespace Kurkku.Messages.Outgoing
{
    class ActivityPointNotificationComposer : IMessageComposer
    {
        private Dictionary<SeasonalCurrencyType, int> currencies;

        public ActivityPointNotificationComposer(Dictionary<SeasonalCurrencyType, int> currencies)
        {
            this.currencies = currencies;
        }

        public override void Write()
        {
            m_Data.Add(currencies.Count);

            foreach (var currency in currencies)
            {
                m_Data.Add((int)currency.Key);
                m_Data.Add(currency.Value);
            }
        }
    }
}
