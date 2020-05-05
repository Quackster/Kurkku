using System;
using System.Collections.Generic;
using Kurkku.Storage.Database.Data;

namespace Kurkku.Messages.Outgoing
{
    class CatalogueClubMessageComposer : IMessageComposer
    {
        private List<CatalogueSubscriptionData> subscriptions;

        public CatalogueClubMessageComposer(List<CatalogueSubscriptionData> subscriptionData)
        {
            this.subscriptions = subscriptionData;
        }

        public override void Write()
        {
            m_Data.Add(subscriptions.Count);

            /* SENT 2677 / [0][0][0]�
             * [10]u
             * [0][0][0][3]
             * 
             * [0][0][8]F[0][9]DEAL_HC_1[0][0][0]d[0][0][0][0][2][0][0][0][1][0][0][0][1][0][0][0][0][0][0][0][0][0][0][7]�[0][0][0][3][0][0][0][0][0][8]G[0][9]DEAL_HC_3[0][0][1]�[0][0][0][0][2][0][0][0][6][0][0][0][6][0][0][0][0][0][0][0][0][0][0][7]�[0][0][0][3][0][0][0][0][0][8]H[0][9]DEAL_HC_2[0][0][0]�[0][0][0][0][2][0][0][0][3][0][0][0][3][0][0][0][0][0][0][0][0][0][0][7]�[0][0][0][3][0][0][0][0][0][0][1]*/

            foreach (var subscription in subscriptions)
            {
                m_Data.Add(subscription.Id);
                m_Data.Add("DEAL_HC_" + subscription.Months);
                m_Data.Add(subscription.PriceCoins);
                m_Data.Add(subscription.PriceSeasonal);
                m_Data.Add((int)subscription.SeasonalType);
                m_Data.Add(true); // public function get vip():Boolean
                m_Data.Add(subscription.Months);

                m_Data.Add(0); // ??
                m_Data.Add(0); // ??

                var futureDate = DateTime.Now.AddMonths(subscription.Months);

                m_Data.Add(futureDate.Year);
                m_Data.Add(futureDate.Month);
                m_Data.Add(futureDate.Day);
            }

            m_Data.Add(1);
        }
    }
}
